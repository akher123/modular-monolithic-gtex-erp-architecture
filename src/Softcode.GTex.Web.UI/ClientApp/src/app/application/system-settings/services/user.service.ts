import { Injectable } from '@angular/core';
import { ApiHttpService } from './../../../shared/services/api-http.service'
import { Observable } from 'rxjs';
import { UserType } from '../../application-shared/components/titlebar/utilities';

@Injectable()
export class UserService {
   

    constructor(private apiHttpService: ApiHttpService) {

    }    

    //getUserList(params: any) {
        
    //    let url = 'system-settings/users/get-active-user-list';
    //    return this.apiHttpService.GETGridData(url, params);
    //}

    getUserDetailEntity(id: any) {              
        let url = 'system-settings/users/get-user-detail?id=' + id;
        return this.apiHttpService.GET(url);
    }

    getContactDetailForUserCreation(id: any) {        
        let url = 'system-settings/users/get-contact-for-user-detail?id=' + id;
        return this.apiHttpService.GET(url);
    }

    getUserRoleAndTitleSelectItems(userId:string, businessProfileIds: number[]) {
        let queryString: string = "";

        businessProfileIds.forEach((item) => {
            queryString += "bps=" + item + "&";
        })
        queryString += 'userId=' + userId;
        //let url = 'system-settings/users/get-select-box-data-for-user-by-bpids?' + bps;
        let url = 'system-settings/users/get-user-role-and-title-data-by-bpids?' + queryString ;
        return this.apiHttpService.GET(url);
    }

    createUser(data: any) {
        let url = 'system-settings/users/insert-user-detail';
        return this.apiHttpService.POST(url, data);
    }

    updateUser(id: any, data: any) {        
        let url = 'system-settings/users/update-user-detail/' + id;
        return this.apiHttpService.PUT(url, data);
    }

    deleteEntity(id: any) {
        let url = 'system-settings/users/delete-user/' + id;
        return this.apiHttpService.DELETE(url);
    }

    deleteEntities(ids: any[]) {
        let url = 'system-settings/users/delete-users/';
        return this.apiHttpService.POST(url, ids);
    }

    getEntitiesDataSource(ids: any[], userType: any) {
        let url = '';
        if (userType == UserType.Contact) {
            url = 'system-settings/users/get-contact-select-items';
            
        }
        else {
            url = 'system-settings/users/get-employee-select-items';
        }
        
        return this.apiHttpService.POST(url, ids);
    }

    changePassword(data: any)
    {
        let url = 'application-service/account/change-password';
        return this.apiHttpService.POST(url, data);
    }

    getChangePassword()
    {
        let url = 'application-service/account/get-change-password';
        return this.apiHttpService.GET(url);
    }
}
