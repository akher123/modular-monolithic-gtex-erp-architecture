import { Injectable } from '@angular/core';
import { ApiHttpService } from './../../../shared/services/api-http.service'
import { Observable } from 'rxjs';

@Injectable()
export class UtilityService {

   
    constructor(private apiHttpService: ApiHttpService) {
    }

     
    getDataSource(serviceUrl: string) {
        
        return this.apiHttpService.GET(serviceUrl);
    }

}


