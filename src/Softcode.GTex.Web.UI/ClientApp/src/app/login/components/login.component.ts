import { Component } from '@angular/core';
import { AuthService } from './../../shared/services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
/** login component*/
export class LoginComponent {
    /** login ctor */
  constructor(public authService: AuthService) {

  }

  login()
  {
    this.authService.login();
  }
}
