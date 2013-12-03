using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using BusinessLogic;
using DataAccessLayer.Database;
using MapperService.Factory;
using Mappers.Base;
using Mappers.Factory;
using Microsoft.Practices.Unity;
using Setup.Ioc;
using Shared;
using Shared.Log;
using WorkoutPlanner.Services.Attributes;
using IMapperFactory = Mappers.Factory.IMapperFactory;

namespace WorkoutPlanner.Services.Inspectors
{
    public class ServiceFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var serviceHost = new MyServiceHost(serviceType, baseAddresses);

            UnityConfiguration.Initialize();
            MapperConfiguration.Initialize(UnityConfiguration.Container.Resolve<IMapperFactory>());
            UnityConfiguration.Container.RegisterType<MapperService.Factory.IMapperFactory, MapperService.Factory.MapperFactory>();
            UnityConfiguration.Container.RegisterType<IUserProvider, WebServiceUserProvider>();
            MapperService.Base.MapperConfiguration.Initialize(UnityConfiguration.Container.Resolve<MapperService.Factory.IMapperFactory>());
            UnityConfiguration.Container.Resolve<IDatabaseContext>().InitializeDatabase();

            serviceHost.Container = UnityConfiguration.Container;
            return serviceHost;
        }
    }

    public class MyServiceHost : ServiceHost
    {
        public MyServiceHost()
        {
            Container = new UnityContainer();
        }

        public MyServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
        {
            Container = new UnityContainer();
        }

        public IUnityContainer Container { get; set; }

        protected override void OnOpening()
        {
            base.OnOpening();

            if (Description.Behaviors.Find<LogginBehavior>() == null)
            {
                Description.Behaviors.Add(new LogginBehavior(Container));
            }
        }
    }

    public class LogginBehavior : IServiceBehavior
    {
        public LogginBehavior()
        {
            
            InstanceProvider = new UnityInstanceProvider();
        }

        public LogginBehavior(IUnityContainer unity)
        {
            InstanceProvider = new UnityInstanceProvider { Container = unity };
        }

        public UnityInstanceProvider InstanceProvider { get; set; }
        

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            InstanceProvider.ServiceType = serviceDescription.ServiceType;

            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                var cd = cdb as ChannelDispatcher;
                if (cd != null)
                {
                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        ed.DispatchRuntime.InstanceProvider = InstanceProvider;
                    }
                }
            }

            var log = InstanceProvider.Container.Resolve<ILog>();
            foreach (ServiceEndpoint endpoint in serviceDescription.Endpoints)//Loop all services
            {
                foreach (OperationDescription operation in endpoint.Contract.Operations)//Loop all operations
                {
                    if (IsAllowingAutomaticLogBehavior(operation))
                    {
                        operation.Behaviors.Add(new LoggingOperationBehavior(log));
                    }
                }
            }
        }

        private static bool IsAllowingAutomaticLogBehavior(OperationDescription operation)
        {
            //Verify if we have the No Log Attribute
            bool allowLoggin = true;
            MethodInfo method = operation.SyncMethod ?? operation.BeginMethod;
            if (method != null)
            {
                allowLoggin = method.CustomAttributes.All(d => d.AttributeType != typeof (NoLogAttribute));
            }
            //Be sure we do not have already the logging operation behavior
            allowLoggin = allowLoggin && !operation.Behaviors.Any(d => d is LoggingOperationBehavior);
            return allowLoggin;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }

    public class UnityInstanceProvider : IInstanceProvider
    {
        public IUnityContainer Container { set; get; }
        public Type ServiceType { set; get; }


        public UnityInstanceProvider()
            : this(null)
        {
        }

        public UnityInstanceProvider(Type type)
        {
            ServiceType = type;
            Container = new UnityContainer();
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return Container.Resolve(ServiceType);
        }

        public object GetInstance(System.ServiceModel.InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {

            var myInstance = instance as IDisposable;

            if (myInstance != null)
            {
                myInstance.Dispose();
            }
        }
    }

    public class LoggingOperationBehavior : IOperationBehavior
    {
        private readonly ILog _myLog;

        public LoggingOperationBehavior(ILog myLog)
        {
            _myLog = myLog;
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new LoggingOperationInvoker(_myLog, dispatchOperation.Invoker, dispatchOperation);
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }
    }

    public class LoggingOperationInvoker : IOperationInvoker
    {
        private readonly IOperationInvoker _baseInvoker;
        private readonly string _operationName;
        private readonly string _controllerName;
        private readonly ILog _myLog;

        public LoggingOperationInvoker(ILog myLog, IOperationInvoker baseInvoker, DispatchOperation operation)
        {
            _myLog = myLog;
            _baseInvoker = baseInvoker;
            _operationName = operation.Name;
            _controllerName = operation.Parent.Type == null ? "[None]" : operation.Parent.Type.FullName;
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            _myLog.Log(string.Format("Method {0} of class {1} called", _operationName, _controllerName));
            try
            {
                return _baseInvoker.Invoke(instance, inputs, out outputs);
            }
            catch (Exception ex)
            {
                _myLog.Log(ex);
                throw;
            }
        }

        public object[] AllocateInputs() { return _baseInvoker.AllocateInputs(); }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            _myLog.Log(string.Format("Method {0} of class {1} called", _operationName, _controllerName));
            return _baseInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result) { return _baseInvoker.InvokeEnd(instance, out outputs, result); }

        public bool IsSynchronous { get { return _baseInvoker.IsSynchronous; } }
    }
}