import { Component, AfterViewInit, ViewChild, OnInit, Input, AfterContentChecked } from '@angular/core';
import { DxValidationGroupComponent } from 'devextreme-angular';
import { CompanyService } from './../../services/company.service';
import { MessageService } from './../../../../shared/services/message.service';
import { TitlebarComponent } from './../../../application-shared/components/titlebar/titlebar.component';
import { ActivatedRoute, Router } from '@angular/router';
import { ToolbarType, DetailPageAction, PatterMatch } from './../../../application-shared/components/titlebar/utilities';
import { NavigationService } from '../../../../shared/services/navigation.service';

import { ImageUploadComponent } from '../../../application-shared/components/common/image-upload.component';

@Component({
    selector: 'app-company-form',
    templateUrl: './company-form.component.html',
    styleUrls: ['./company-form.component.scss'],
})

export class CompanyFormComponent implements AfterContentChecked, OnInit {
        
    @Input('CompanyId') companyId = 0;
    toolbarType: ToolbarType = ToolbarType.DetailPage;
    company: any = {};
    emptyCompany: any = {};
    businessProfileSelectItems: any = [];
    counrtySelectItems: any = [];
    organisationTypeSelectItems: any = [];
    preferredContactMethodSelectItems: any = [];
    industrySelectItems: any = [];
    ratingSelectItems: any = [];
    stateSelectItems: any = [];
    relationshipTypeSelectItems: any = [];
    contentClass: any = "detail-page-content-div";
    photoDataSource: any = [];
    isDefaultBusinessProfileUser: boolean = false;
    disabledBusinessProfileUser = true;

    emailPattern: any = PatterMatch.EmailPattern;

    @ViewChild(TitlebarComponent)
    private titlebar: TitlebarComponent;

    @ViewChild('imgUploadControl')
    private imageUploadControl: ImageUploadComponent;

    @ViewChild('formValidation')
    private formValidation: DxValidationGroupComponent;

    constructor(private companyService: CompanyService
        , private messageService: MessageService
        , private navigationService: NavigationService
        , private route: ActivatedRoute
        , private router: Router) {

        this.route.params.subscribe(params => {
            if (params['companyId'] !== undefined) {
                this.companyId = params['companyId'];
                this.toolbarType = ToolbarType.DetailTabPage;
                //this.contentClass = "detail-page-content-div-tab";
            }
        });
    }

    ngOnInit(): void {
        this.loadCompanyData();
    }

    ngAfterContentChecked(): void {
        this.disabledBusinessProfileUser = this.companyId > 0 || !this.isDefaultBusinessProfileUser;

        if (this.companyId == 0 && this.company.countryId<1) {
             
            var defaultCountries = this.counrtySelectItems.filter(x => x.isDefault);
            if (defaultCountries.length > 0) {
                this.company.countryId = defaultCountries[0].id;
            }
        }
    }

    onClickNotify(): void {
        if (this.companyId == null) {
            this.companyService.createCompany(this.company).subscribe(() => {
                this.messageService.success('Company save successfully', 'Company save');
            });
        } else {
            this.companyService.updateCompany(this.companyId, this.company).subscribe(() => {
                this.messageService.success('Company update successfully', 'Company Update');
            });
        }
    }


    loadCompanyData(): void {
        this.companyService.getCompany(this.companyId).subscribe(data => {

            if (data == undefined || data == null) return;

            this.emptyCompany = data.result.emptyCompany;
            this.setCompanyDataSource(data.result.company);

            this.isDefaultBusinessProfileUser = data.result.isDefaultBusinessProfile;
            this.businessProfileSelectItems = data.result.businessProfileSelectItems;
            this.counrtySelectItems = data.result.counrtySelectItems;
            this.relationshipTypeSelectItems = data.result.relationshipTypeSelectItems;
            
            

            this.titlebar.initializeToolbar(this.companyId == 0 ? "Company: New" : "Company: " + this.company.companyName, null, ToolbarType.DetailPage);
            
        });
    }

    setCompanyDataSource(company: any): void {
        this.company = company;
        this.companyId = company.id;
        
        this.photoDataSource = company.logo;
      
        this.imageUploadControl.setEntityValue(this.photoDataSource);
    }

    onBusinessProfileSelectionChanged(e): void {
        if (e.value > 0) {
            this.companyService.getCompanySelectItems(e.value).subscribe(data => {
                this.organisationTypeSelectItems = data.result.organisationTypeSelectItems;
                this.preferredContactMethodSelectItems = data.result.preferredContactMethodSelectItems;
                this.industrySelectItems = data.result.industrySelectItems;
                this.ratingSelectItems = data.result.ratingSelectItems;
            });
        }
        else {
            this.organisationTypeSelectItems = [];
            this.preferredContactMethodSelectItems = [];
            this.industrySelectItems = [];
            this.ratingSelectItems = [];
        }
    }

    onCountrySelectionChanged(e): void {
        if (e.value > 0) {
            this.companyService.getStateByCountry(e.value).subscribe(data => {
                this.stateSelectItems = data.result;
            });
        }
        else {
            this.stateSelectItems = [];
        }
    }

    validateAndSave(action: DetailPageAction): void {
        if (!this.formValidation.instance.validate().isValid) {
            return;
        }

        this.saveEntity(action);
    }


    saveEntity(action: DetailPageAction): void {

        this.company.photo = this.imageUploadControl.photoDataSource;       

        if (this.companyId == 0) {
            this.companyService.createCompany(this.company).subscribe(data => {
                this.messageService.success("Record has been saved successfully", 'Information');
                this.companyId = data.result;                
                this.redirectToListPage(action);
            });
        }
        else {
            this.companyService.updateCompany(this.companyId, this.company).subscribe(data => {
                this.messageService.success("Record has been updated successfully", 'Information');
                this.companyId = data.result;
                this.imageUploadControl.photoDataSource.isUpdated = false;
                this.redirectToListPage(action);
            });
        }
    }

    redirectToListPage(action: DetailPageAction): void {
        var newNavigationUrl = '/crm/company';

        if (action == DetailPageAction.Close || action == DetailPageAction.SaveAndClose) {
            this.navigationService.navigateToReturnurl(this.router.url);
        }
        else if (action == DetailPageAction.SaveAndNew) {
            this.setCompanyDataSource(JSON.parse(JSON.stringify(this.emptyCompany)));

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl, this.router.url);
        }
        else if (action == DetailPageAction.Save && newNavigationUrl == this.router.url) {
            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl + '/' + this.companyId, this.router.url);
        }
        else {
            this.titlebar.setToolbarTitle("Company: " + this.company.companyName);
        }
    }

    onSaveClicked(e): void {
        this.validateAndSave(DetailPageAction.Save);
    }

    onSaveNNewClicked(e): void {
        this.validateAndSave(DetailPageAction.SaveAndNew);
    }

    onSaveNCloseClicked(e): void {
        this.validateAndSave(DetailPageAction.SaveAndClose);
    }

    onCloseClicked(e): void {
        this.redirectToListPage(DetailPageAction.Close);
    }
}
