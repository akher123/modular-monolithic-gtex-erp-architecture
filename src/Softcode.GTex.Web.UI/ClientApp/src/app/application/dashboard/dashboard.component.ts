import { Component } from '@angular/core';
import { alert } from 'devextreme/ui/dialog';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
/** dashboard component*/
export class DashboardComponent {
    /** dashboard ctor */
  constructor(public oidcSecurityService: OidcSecurityService) {
    //this.oidcSecurityService.authorize();    
  }

}
