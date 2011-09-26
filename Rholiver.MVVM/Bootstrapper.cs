using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Threading;
using Ninject;
using Ninject.Activation.Strategies;
using Rholiver.Mvvm.Events;
using Rholiver.Mvvm.Infrastructure;
using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Navigation;
using Rholiver.Mvvm.Views;

namespace Rholiver.Mvvm
{
    public abstract class Bootstrapper<T> where T : IViewModel
    {
        private readonly IKernel _kernel;

        protected Bootstrapper() {
            _kernel = InitializeKernel();

            InitializeApplication();
        }

        private IKernel InitializeKernel() {
            var kernel = new StandardKernel();

            kernel.Bind<IKernel>().ToConstant(kernel);
            kernel.Bind<Application>().ToConstant(Application.Current);
            kernel.Bind<Dispatcher>().ToConstant(Deployment.Current.Dispatcher);
            kernel.Bind<IViewBuilder>().To<ViewBuilder>();
            kernel.Bind<IViewLocator>().To<ViewLocator>();
            kernel.Bind<IViewWeaver>().To<ViewWeaver>();
            kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            kernel.Components.Add<IActivationStrategy, SubcribeToEventAggregatorStrategy>();
            kernel.Bind<IDialogManager>().To<DialogManager>();
            kernel.Bind<IElementManager>().To<ElementManager>();
            kernel.Bind<IMultiElementManager>().To<MultiElementManager>();

            kernel.Bind(typeof (IProvider<,>)).To(typeof (NinjectProvider<,>));

            kernel.Bind<IPropertyBinder>().ToConstant(new ControlBinder<TextBlock> {ControlProperty = TextBlock.TextProperty});
            kernel.Bind<IPropertyBinder>().ToConstant(new ControlBinder<TextBox> {ControlProperty = TextBox.TextProperty});
            kernel.Bind<IPropertyBinder>().ToConstant(new ControlBinder<ContentControl> {ControlProperty = ContentControl.ContentProperty});
            kernel.Bind<IPropertyBinder>().ToConstant(new ControlBinder<ItemsControl> {ControlProperty = ItemsControl.ItemsSourceProperty});
            kernel.Bind<IPropertyBinder>().ToConstant(new ControlBinder<Button> {ControlProperty = ContentControl.ContentProperty});
            
            InitialiseDependancies(kernel);

            return kernel;
        }

        protected virtual void InitialiseDependancies(IKernel kernel) {}

        private void InitializeApplication() {
            var application = _kernel.Get<Application>();
            if (application != null) {
                application.Startup += ApplicationStartup;
                application.Exit += ApplicationExit;
                application.UnhandledException += ApplicationUnhandledException;
            }
        }

        private void ApplicationStartup(object sender, StartupEventArgs e) {
            var viewBuilder = _kernel.Get<IViewBuilder>();
            var modelAndView = viewBuilder.Build<T>();

            var application = _kernel.Get<Application>();
            application.RootVisual = modelAndView.View;
        }

        private void ApplicationExit(object sender, EventArgs e) {}

        private void ApplicationUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!Debugger.IsAttached) {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDom(e));
            }
        }

        private static void ReportErrorToDom(ApplicationUnhandledExceptionEventArgs e) {
            try {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch {}
        }
    }
}