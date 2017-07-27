using System;
using System.Web.Mvc;
using BasicIocContainer;
using MoviesApp.data;
using MoviesApp.Services;

namespace MoviesApp
{
    public class BasicIocControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        public BasicIocControllerFactory(IContainer container)
        {
            _container = container;
            RegisterServices();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext,
            Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            IController controller = (IController) _container.Resolve(controllerType);
            if (controller != null)
            {
                return controller;
            }
            return base.GetControllerInstance(requestContext, controllerType);
        }

        private void RegisterServices()
        {
            _container.Register<IMovieService, MovieServiceImpl>();
            _container.Register<IMovieRepository, MovieRepositoryImpl>(LifeCycle.Singleton);
        }
    }
}