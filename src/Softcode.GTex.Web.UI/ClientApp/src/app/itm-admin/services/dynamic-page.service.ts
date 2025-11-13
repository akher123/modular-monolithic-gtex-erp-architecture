import { Injectable } from '@angular/core'; 
import { Observable } from 'rxjs';
import { ApiHttpService } from '../../shared/services/api-http.service';

@Injectable()
export class dynamicPageService {
  
    constructor(private apiHttpService: ApiHttpService) {
    }

    getPageInfoByRoutingUrl(routingUrl: any) {

        let url = 'application-service/application-page/get-application-detail-page-by-routing-url?routingUrl=' + routingUrl;
        return this.apiHttpService.GET(url);
    }

    getPageInfoByName(name: any) {

        let url = 'application-service/application-page/get-application-detail-page-by-routing-url?name=' + name;
        return this.apiHttpService.GET(url);
    }

    getListPageInfoById(id: any) {

        let url = 'application-service/application-page/get-application-list-page-by-id/' + id;
        return this.apiHttpService.GET(url);
    }

    //getDxGridRecords(url: any, params: any) {
    //    return this.apiHttpService.GETDXGrid(url, params);
    //}

    //deleteRecord(url: any, id: any) {
    //    return this.apiHttpService.DELETE(url + id);
    //}

    //deleteRecords(url: any, ids: any[]) {
    //    return this.apiHttpService.POST(url, ids);
    //}
}
