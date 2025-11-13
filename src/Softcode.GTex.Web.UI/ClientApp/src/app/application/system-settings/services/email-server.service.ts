import { Injectable } from '@angular/core';
import { ApiHttpService } from './../../../shared/services/api-http.service'
import { Observable } from 'rxjs';

@Injectable()
export class EmailServerService {

    url: string = '';

    constructor(private apiHttpService: ApiHttpService)
    {

    }
     
  
    getRecordInfoById(recordId: number): any {
        this.url = 'messaging/email-server/get-email-server/' + recordId;
        return this.apiHttpService.GET(this.url);
    }

    createRecord(formDataSource: any): any {
        this.url = 'messaging/email-server/create-email-server';
        return this.apiHttpService.POST(this.url, formDataSource);
    }

    updateRecord(recordId: number, formDataSource: any): any {
        this.url = 'messaging/email-server/update-email-server/' + recordId;
        return this.apiHttpService.PUT(this.url, formDataSource);
    }

    sendTestEmail(formDataSource:any): any {
        this.url = 'messaging/email-server/send-test-email';
        return this.apiHttpService.POST(this.url, formDataSource);
    }

}
