using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GigaApp.Domain.UseCases.SignIn;
using GigaApp.Domain.UseCases.SignOn;
using GigaApp.XAF.Blazor.Server.Authentication;
using MediatR;

namespace GigaApp.XAF.Blazor.Server.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AuthorizationController : ViewController
    {
        private readonly IMediator useCase;
        private readonly IAuthTokenStorage tokenStorage;
        private readonly IHttpContextAccessor accessor;

        [ActivatorUtilitiesConstructor]
        public AuthorizationController(IMediator useCase, IAuthTokenStorage tokenStorage, IHttpContextAccessor accessor) : this()
        {
            this.useCase = useCase;
            this.tokenStorage = tokenStorage;
            this.accessor = accessor;
        }
        public AuthorizationController()
        {
            InitializeComponent();


            SimpleAction signInAction = new SimpleAction(this, "SignInActionAction", PredefinedCategory.View)
            {

                Caption = "Войти под тестовым пользователем",

            };
            
            signInAction.Execute += SignInAction_Execute;

            SimpleAction signOnAction = new SimpleAction(this, "SignOnActionAction", PredefinedCategory.View)
            {

                Caption = "Зарегистрировать тестового пользователя",

            };

            signOnAction.Execute += SignOnAction_Execute;
        }

        private void SignOnAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var result = Task.Run(() => useCase
                .Send(new SignOnCommand("Test", "123"), CancellationToken.None)).Result;
        }

        
        private void SignInAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var result = Task.Run(() => useCase
                .Send(new SignInCommand("Test", "123"), CancellationToken.None)).Result;

            //tokenStorage.Store(accessor.HttpContext!,result.token);

            using var httpClient = new HttpClient();

            var task = Task.Run(()=>httpClient.PostAsync(
                "https://localhost:44318/accountblazor/signin", JsonContent.Create(new { Token = result.token }))).Result;

        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
