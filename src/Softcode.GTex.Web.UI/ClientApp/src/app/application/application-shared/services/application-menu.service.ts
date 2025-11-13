import { Injectable } from '@angular/core';
import { ApiHttpService } from './../../../shared/services/api-http.service'
import { Observable } from 'rxjs';

@Injectable()
export class ApplicationMenuService {

    url: string = '';

    constructor(private apiHttpService: ApiHttpService) {
    }

    getCurrentUserAppMenu() {        
        this.url = 'application-service/application-menu/get-application-menu';        
        return this.apiHttpService.GET(this.url);
    }

    getApplicationTeeList() {
      let url = 'application-service/application-menu/get-application-menu-tree-list';
      return url;
    }

    createApplicationMenu(data: any) {
      this.url = 'application-service/application-menu/create-application-menu';
      return this.apiHttpService.POST(this.url, data);
    }

    updateApplicationMenu(id: number, data: any) {
      this.url = 'application-service/application-menu/update-application-menu/' + data.id;
      return this.apiHttpService.PUT(this.url, data);
    }
}


