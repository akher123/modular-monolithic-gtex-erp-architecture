import { Injectable, Component, OnInit, OnDestroy } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Subscription } from 'rxjs';

import { OidcSecurityService, OpenIDImplicitFlowConfiguration, OidcConfigService, AuthWellKnownEndpoints } from 'angular-auth-oidc-client';


@Injectable()
export class AuthService implements OnInit, OnDestroy
{
    isAuthorizedSubscription: Subscription;
    public isAuthorized: boolean;

    constructor(public oidcSecurityService: OidcSecurityService)
    {
    }

    ngOnInit(): void
    {
      this.isAuthorizedSubscription = this.oidcSecurityService.getIsAuthorized().subscribe(
        (isAuthorized: boolean) =>
        {
          this.isAuthorized = isAuthorized;
        });

      if (window.location.hash) {
        this.oidcSecurityService.authorizedCallback();
      }
    }

    ngOnDestroy(): void
    {
      this.isAuthorizedSubscription.unsubscribe();
    }

    authorizedCallback()
    {
      this.oidcSecurityService.authorizedCallback();
    }

    getIsAuthorized(): Observable<boolean>
    {
      return this.oidcSecurityService.getIsAuthorized();
    }

    getUserData(): Observable<any>
    {
      return this.oidcSecurityService.getUserData();
    }

     getToken(): string
    {
      return this.oidcSecurityService.getToken();
    }

    login()
    {
      this.oidcSecurityService.authorize();
    }

    refreshSession()
    {
      this.oidcSecurityService.authorize();
    }

    logout()
    {
      this.oidcSecurityService.logoff();
    }

    private setRequestOptions(options?: RequestOptions)
    {
      if (options) {
        this.appendAuthHeader(options.headers);
      }
      else {
        options = new RequestOptions({ headers: this.getHeaders(), body: "" });
      }
      return options;
    }

    private getHeaders()
    {
      let headers = new Headers();
      headers.append('Content-Type', 'application/json');
      this.appendAuthHeader(headers);
      return headers;
    }

    private appendAuthHeader(headers: Headers)
    {
      const token = this.oidcSecurityService.getToken();

      if (token == '') return;

      const tokenValue = 'Bearer ' + token;
      headers.append('Authorization', tokenValue);
    }
}
