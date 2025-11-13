import { Injectable } from '@angular/core';
import { ApiHttpService } from './../../../shared/services/api-http.service'
import { Observable } from 'rxjs';

@Injectable()
export class CompanyService
{

    constructor(private apiHttpService: ApiHttpService)
    {
    }

    getDxGridCompanies(params: any)
    {
        let url = 'crm/company/get-companies';
        return this.apiHttpService.GETDXGrid(url, params);
    }

    getCompanies()
    {
        let url = 'crm/company/get-companies';
        return this.apiHttpService.GET(url);
    }

    getCompanySelectItems(businessProfileId: number)
    {
        let url = 'crm/company/get-company-select-items/' + businessProfileId;
        return this.apiHttpService.GET(url);
    }

    getCompanySelectItemsByBusinessProfile(ids: any[]) {

        let url = 'crm/contact/get-contact-company-list';
        return this.apiHttpService.POST(url, ids);
    }

    getCompany(id: any)
    {
        let url = 'crm/company/get-company?id=' + id;
        return this.apiHttpService.GET(url);
    }

    createCompany(data: any)
    {
        let url = 'crm/company/create-company';
        return this.apiHttpService.POST(url, data);
    }

    updateCompany(id: any, data: any)
    {
        let url = 'crm/company/update-company/' + id;
        return this.apiHttpService.PUT(url, data);
    }

    deleteCompany(id: any)
    {
        let url = 'crm/company/delete-company/' + id;
        return this.apiHttpService.DELETE(url);
    }

    deleteCompanies(ids: any[])
    {
        let url = 'crm/company/delete-companies/';
        return this.apiHttpService.POST(url, ids);
    }

    getStateByCountry(countryId: number)
    {
        let url = 'application-service/address-database/get-state-by-country/' + countryId;
        return this.apiHttpService.GET(url);
    }

    getPageDetails(id: any)
    {
        let url = 'crm/company/get-company-details-tab/' + id;
        return this.apiHttpService.GET(url);
    }
}


