import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER, ErrorHandler, ApplicationRef } from '@angular/core';
import { Configuration } from './app.constants';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApiHttpService } from './shared/services/api-http.service'
import { MessageService } from './shared/services/message.service'
import { NavigationService } from './shared/services/navigation.service'
import { DxDataGridModule, DxValidationGroupModule } from 'devextreme-angular';
import { Http, HttpModule, URLSearchParams } from '@angular/http';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgHttpLoaderModule } from 'ng-http-loader/ng-http-loader.module';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccordionModule } from 'ngx-bootstrap';
import { AuthModule, OidcSecurityService, OpenIDImplicitFlowConfiguration, OidcConfigService, AuthWellKnownEndpoints } from 'angular-auth-oidc-client';
import { AuthService } from './shared/services/auth.service';
import { AuthorizationGuard } from './shared/services/authorization.guard';
import { AuthorizationCanGuard } from './shared/services/authorization.can.guard';
import { AuthInterceptor } from './shared/services/auth-interceptor';
import { LoginModule } from './login/login.module';
import { ShowYesNoDirective } from './shared/directives/show-yes-no.directive'
import { GlobalErrorHandlerService } from './shared/services/global-error-handler.service';
import { ClipboardModule } from 'ngx-clipboard';
import { environment } from '../environments/environment';
import { CanDeactivateGuard } from './shared/can-deactivate/can-deactivate.guard';

//import { ServerErrorsInterceptor } from './shared/services/server-errors-interceptor';

//export function loadConfig(oidcConfigService: OidcConfigService)
//{
//  console.log('APP_INITIALIZER STARTING');
//  return () => oidcConfigService.load_using_stsServer('http://localhost:6066');
//}

export function loadConfig(oidcConfigService: OidcConfigService)
{
    return () => oidcConfigService.load(environment.identityUrl);
}


// AoT requires an exported function for factories
export function createTranslateLoader(http: HttpClient) {
    // for development
    // return new TranslateHttpLoader(http, '/start-angular/SB-Admin-BS4-Angular-5/master/dist/assets/i18n/', '.json');
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
    declarations: [
        AppComponent
        , ShowYesNoDirective
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        AppRoutingModule,
        HttpClientModule,
        DxDataGridModule,
        HttpModule,
        CommonModule,
        NgHttpLoaderModule,
        ReactiveFormsModule,
        FormsModule,
        ToastrModule.forRoot({
            closeButton: true,
            positionClass: 'toast-bottom-right',
            easing: 'ease-in',
            easeTime: 500,
            enableHtml: true,
            timeOut: 1500,
            extendedTimeOut: 1000
        }),
        BrowserAnimationsModule,
        AccordionModule.forRoot(),
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: createTranslateLoader,
                deps: [HttpClient]
            }
        }),
        AuthModule.forRoot()
        , DxValidationGroupModule
        , ClipboardModule
    ],
    providers: [ApiHttpService
        , MessageService
        , NavigationService
        , AuthService
        , OidcConfigService
        , AuthorizationGuard
        , AuthorizationCanGuard
        , CanDeactivateGuard
        , {
            provide: APP_INITIALIZER,
            useFactory: loadConfig,
            deps: [OidcConfigService],
            multi: true
        }
        //, {
        //    provide: HTTP_INTERCEPTORS,
        //    useClass: ServerErrorsInterceptor,
        //    multi: true,
        //}
        , {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        }
        , Configuration
        //, GlobalErrorHandlerService
        //, {
        //    provide: ErrorHandler,
        //    useClass: GlobalErrorHandlerService,
        //}
    ],
    exports: [
        ShowYesNoDirective
    ],
    bootstrap: [AppComponent]
})

export class AppModule {
    constructor(
        private oidcSecurityService: OidcSecurityService,
        private oidcConfigService: OidcConfigService,
        configuration: Configuration,
    ) {
        this.oidcConfigService.onConfigurationLoaded.subscribe(() => {

            const openIDImplicitFlowConfiguration = new OpenIDImplicitFlowConfiguration();
            openIDImplicitFlowConfiguration.stsServer = this.oidcConfigService.clientConfiguration.stsServer;
            openIDImplicitFlowConfiguration.redirect_url = this.oidcConfigService.clientConfiguration.redirect_url;

            // The Client MUST validate that the aud (audience) Claim contains its client_id value registered at the Issuer
            // identified by the iss (issuer) Claim as an audience.
            // The ID Token MUST be rejected if the ID Token does not list the Client as a valid audience,
            // or if it contains additional audiences not trusted by the Client.
            openIDImplicitFlowConfiguration.client_id = this.oidcConfigService.clientConfiguration.client_id;
            openIDImplicitFlowConfiguration.response_type = this.oidcConfigService.clientConfiguration.response_type;
            openIDImplicitFlowConfiguration.scope = this.oidcConfigService.clientConfiguration.scope;
            openIDImplicitFlowConfiguration.post_logout_redirect_uri = this.oidcConfigService.clientConfiguration.post_logout_redirect_uri;
            openIDImplicitFlowConfiguration.start_checksession = this.oidcConfigService.clientConfiguration.start_checksession;

            openIDImplicitFlowConfiguration.silent_renew = this.oidcConfigService.clientConfiguration.silent_renew;
            openIDImplicitFlowConfiguration.silent_renew_url = this.oidcConfigService.clientConfiguration.silent_renew_url;
            openIDImplicitFlowConfiguration.silent_redirect_url = this.oidcConfigService.clientConfiguration.silent_renew_url;
            
            openIDImplicitFlowConfiguration.post_login_route = this.oidcConfigService.clientConfiguration.startup_route;
            // HTTP 403
            openIDImplicitFlowConfiguration.forbidden_route = this.oidcConfigService.clientConfiguration.forbidden_route;
            // HTTP 401
            openIDImplicitFlowConfiguration.unauthorized_route = this.oidcConfigService.clientConfiguration.unauthorized_route;
            openIDImplicitFlowConfiguration.log_console_warning_active = this.oidcConfigService.clientConfiguration.log_console_warning_active;
            openIDImplicitFlowConfiguration.log_console_debug_active = this.oidcConfigService.clientConfiguration.log_console_debug_active;
            // id_token C8: The iat Claim can be used to reject tokens that were issued too far away from the current time,
            // limiting the amount of time that nonces need to be stored to prevent attacks.The acceptable range is Client specific.
            openIDImplicitFlowConfiguration.max_id_token_iat_offset_allowed_in_seconds =
                this.oidcConfigService.clientConfiguration.max_id_token_iat_offset_allowed_in_seconds;
            openIDImplicitFlowConfiguration.storage = localStorage;
            configuration.FileServer = this.oidcConfigService.clientConfiguration.apiFileServer;
            configuration.ApiServer = this.oidcConfigService.clientConfiguration.apiServer;

            const authWellKnownEndpoints = new AuthWellKnownEndpoints();
            authWellKnownEndpoints.setWellKnownEndpoints(this.oidcConfigService.wellKnownEndpoints);

            this.oidcSecurityService.setupModule(openIDImplicitFlowConfiguration, authWellKnownEndpoints);

        });
    }
}
