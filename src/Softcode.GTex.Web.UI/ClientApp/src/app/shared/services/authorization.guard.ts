import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AuthService } from './auth.service';

@Injectable()
export class AuthorizationGuard implements CanActivate
{
    constructor(
        private router: Router,
        private oidcSecurityService: OidcSecurityService,
        private authService: AuthService
    ) { }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean
    {
        return this.oidcSecurityService.getIsAuthorized().pipe(
            map((isAuthorized: boolean) =>
            {
                if (isAuthorized) {
                    return true;
                }


                //this.authService.login();

                this.router.navigate(['/login']);
                return false;
            })
        );
    }
}
