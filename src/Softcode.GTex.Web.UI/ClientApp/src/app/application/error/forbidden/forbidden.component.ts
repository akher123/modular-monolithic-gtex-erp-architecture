import { Component } from '@angular/core';
import { NavigationService } from '../../../shared/services/navigation.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-forbidden',
    templateUrl: './forbidden.component.html',
    styleUrls: ['./forbidden.component.scss']
})
/** forbidden component*/
export class ForbiddenComponent {
    /** forbidden ctor */
    constructor(private navigationService: NavigationService
        , private router: Router)
    {

    }


    goBack(): void {

        this.navigationService.navigateToReturnurl(this.router.url);
    }

        
}
