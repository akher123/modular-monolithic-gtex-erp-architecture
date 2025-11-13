import { Injectable } from '@angular/core';
import { ApiHttpService } from '../../../shared/services/api-http.service';

@Injectable()
export class ContactService {
    
    constructor(private apiHttpService: ApiHttpService) {

    }

    getDxGridRecords(id: any, params: any) {
        let url = 'crm/contact/get-contact-company-list/' + id;
        return this.apiHttpService.GETDXGrid(url, params);
    }

    /**
     * get contact page details
     * @param id
     */
    getContactPageDetails(id: any) {
        let url = 'crm/contact/get-contact-details-tab?id=' + id;
        return this.apiHttpService.GET(url);
    }

    getContact(id: any) {
        let url = 'crm/contact/get-contact?id=' + id;
        return this.apiHttpService.GET(url);
    }


    getContactSelectBoxItems(businessProfileIds: number[]) {
        let bps: string = "";

        businessProfileIds.forEach((item) => {
            bps += "bps=" + item + "&";
        })

        let url = 'crm/contact/get-contact-select-items-by-bpids?' + bps;
        return this.apiHttpService.GET(url);
    }

    //getCompany(id: any) {
    //    this.url = 'crm/contact/get-contact-company-list/' + id;
    //    return this.apiHttpService.GET(this.url);
    //}

    getCompanyByBusinessProfile(ids: any[]) {
        
        let url = 'crm/contact/get-contact-company-list';
        return this.apiHttpService.POST(url, ids);
    }

    createContact(fileName: any, data: any) {
        let url = 'crm/contact/create-contact?imageFileName=' + fileName;
        return this.apiHttpService.POST(url, data);
    }

    updateContact(fileName: any, data: any) {
        let url = 'crm/contact/update-contact?imageFileName=' + fileName;
        return this.apiHttpService.PUT(url, data);
    }
}
