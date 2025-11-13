import { Component, OnInit, Input } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { AuthService } from './../../../../shared/services/auth.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    pushRightClass: string = 'push-right';
    isAuthorizedSubscription: Subscription;
    isAuthorized: boolean;
    userName: string;
    //@Input('ApplicationHeader') applicationHeader: any;
    applicationHeader: any;
    @Input()
    set ApplicationHeader(header: any) {
        this.applicationHeader = header;
        if (header != undefined && header.displayName != undefined) {
            this.userName = header.displayName;
        }
    }

    constructor(private translate: TranslateService, public router: Router, public authService: AuthService) {

        this.router.events.subscribe(val => {
            if (
                val instanceof NavigationEnd &&
                window.innerWidth <= 992 &&
                this.isToggled()
            ) {
                this.toggleSidebar();
            }
        });
    }

    ngOnInit() {
        this.isAuthorizedSubscription = this.authService.getIsAuthorized().subscribe(
            (isAuthorized: boolean) => {
                this.isAuthorized = isAuthorized;
            });
        //this.authService.getUserData().subscribe(
        //    (data: any) => {
        //        this.userInformation = data;
        //    });

    }

    isToggled(): boolean {
        const dom: Element = document.querySelector('body');
        return dom.classList.contains(this.pushRightClass);
    }

    toggleSidebar() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle(this.pushRightClass);
    }

    onLoggedout() {
        localStorage.removeItem('isLoggedin');
    }

    login() {
        this.authService.login();
    }


    logout() {
        this.authService.logout();
    }

    changeLang(language: string) {
        this.translate.use(language);
    }

    changePassword()
    {
        this.router.navigate(['/system-settings/change-password']);
    }
    
}
