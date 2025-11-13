import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest } from '@angular/common/http';
import { AuthService } from './../services/auth.service';
import { Observable } from 'rxjs';


@Injectable()
export class AuthInterceptorService implements HttpInterceptor
{
    private authService: AuthService;

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

        return next.handle(requestToForward);
    }
}
