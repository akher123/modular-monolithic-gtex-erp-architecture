import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { AuthService } from './../services/auth.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MessageService } from './message.service';
import { Router } from '@angular/router';
import {  OK, INTERNAL_SERVER_ERROR, UNAUTHORIZED, NOT_FOUND } from 'http-status-codes'
import { NavigationService } from './navigation.service';


@Injectable()
export class AuthInterceptor implements HttpInterceptor
{
    private authService: AuthService;
    private navigationService: NavigationService;

    constructor(private injector: Injector)
    {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
    {
        let requestToForward = req;

        if (this.authService === undefined) {
            this.authService = this.injector.get(AuthService);
        }
        if (this.authService !== undefined) {
            let token = this.authService.getToken();
            if (token !== "") {
                let tokenValue = "Bearer " + token;
                requestToForward = req.clone({ setHeaders: { "Authorization": tokenValue } });
            }
        } else {
            console.debug("OidcSecurityService undefined: NO auth header!");
        }

        //return next.handle(requestToForward);


        /**
         * continues request execution
         */
        return next.handle(requestToForward).pipe(catchError((error, caught) =>
        {
            //intercept the respons error and displace it to the console
            this.handleAuthError(error);
            return of(error);
        }) as any);
    
    }

    /**
   * manage errors
   * @param response
   * @returns {any}
   */
    private handleAuthError(response: HttpErrorResponse): Observable<any>
    {        
        if (this.navigationService === undefined) {
            this.navigationService = this.injector.get(NavigationService);
        }

        const messageService = this.injector.get(MessageService);
        const router = this.injector.get(Router);

        if (response.status === UNAUTHORIZED) {
            router.navigate(['/login']);
            //this.authService.login();
        }

        if (response.status === 403) {
            //router.navigate(['/error/forbidden']);
            this.navigationService.navigateAndUpdateReturnUrl('/error/forbidden', router.url);
            return of(response.message);
        }

        if (response.status === INTERNAL_SERVER_ERROR) {
            messageService.error(response.error.errorMessage, "");
            return of(response.message);
        }

        if (response.status === NOT_FOUND) {
            messageService.error(response.error.errorMessage, "");
            return of(response.message);
        }

        if (response.error.errorMessage !== null || response.error.errorMessage !== undefined || response.error.errorMessage === '') {
            messageService.error(response.error.errorMessage, "");
        }

        throw response;
    }
}
