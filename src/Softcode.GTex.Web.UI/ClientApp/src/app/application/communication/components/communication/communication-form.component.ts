import { AfterViewInit, Component, Input, ViewChild, AfterViewChecked, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DxValidationGroupComponent, DxValidatorComponent } from 'devextreme-angular';
import "rxjs/add/operator/toPromise";
import { EntitySelectBoxComponent } from '../../../application-shared/components/common/app-entity-select-box.component';
import { FileUploadComponent } from '../../../dms/components/file-upload.component';
import { MessageService } from './../../../../shared/services/message.service';
import { NavigationService } from './../../../../shared/services/navigation.service';
import { TitlebarComponent } from './../../../application-shared/components/titlebar/titlebar.component';
import { DetailPageAction, ToolbarType } from './../../../application-shared/components/titlebar/utilities';
import { CommunicationService } from './../../services/communication.service';
declare var $: any;

@Component({
    selector: 'app-communication-form',
    templateUrl: './communication-form.component.html',
    styleUrls: ['./communication-form.component.scss']
})
export class CommunicationFormComponent implements AfterViewInit {


    @Input() communicationId: number = 0;
    communicationModel: any = {};
    businessProfileSelectItems: any = [];
    communicationForSelectItems: any = [];
    communicationFor: string = "";
    communicationMathodSelectItems: any = [];
    communicationStatusSelectItems: any = [];
    followupByContactSelectItems: any = [];
    entityTypeId: number = 0;
    followupByType = 'Followup Contact';


    hideEntityClass: string = "row form-group hide-div"
    showEntityClass: string = "row form-group show-div";
    entityClass: string = this.showEntityClass;


    hideFollowupCssClass: string = "row form-group hide-div"
    showFollowupCssClass: string = "row form-group show-div";
    followupCssClass: string = this.showFollowupCssClass;

    contentClass: any = "detail-page-content-div";


    toolbarType: ToolbarType = ToolbarType.DetailPage;

    @ViewChild(TitlebarComponent)
    private titlebar: TitlebarComponent;


    @ViewChild('followupByContactSelectionBox')
    private followupByContactSelectionBox: EntitySelectBoxComponent;

    @ViewChild('communicationEntitySelectionBox')
    private communicationEntitySelectionBox: EntitySelectBoxComponent;




    @ViewChild('formValidation')
    private formValidation: DxValidationGroupComponent;

    @ViewChild('subjecctValidation')
    private subjecctValidation: DxValidatorComponent;

    @ViewChild('communicationDateTimeValidation')
    private communicationDateTimeValidation: DxValidatorComponent;

    @ViewChild('businessProfileSelectionBoxValidation')
    private businessProfileSelectionBoxValidation: DxValidatorComponent;


    @ViewChild('fileUploadControl')
    private fileUploadControl: FileUploadComponent


    constructor(private communicationService: CommunicationService,
        private messageService: MessageService,
        private navigationService: NavigationService,
        private route: ActivatedRoute,
        private router: Router) {

        this.route.params.subscribe(params => {
            if (params['communicationId'] !== undefined) {
                this.communicationId = params['communicationId'];
            }
        });
    }

    ngOnInit(): void {
        this.attachValidationToControl();
        this.init();

    }
    ngAfterViewInit(): void {

        if (this.communicationModel.businessProfileId > 0 && this.communicationModel.entityTypeId > 0) {
            this.entityClass = this.showEntityClass;           
        }
        else {
            this.entityClass = this.hideEntityClass;
        }

        if (this.communicationModel.isFollowupEnable) {
            this.followupCssClass = this.showFollowupCssClass;
        }
        else {
            this.followupCssClass = this.hideFollowupCssClass;
        }
    }


    init(): void {

        this.communicationService.getCommunication(this.communicationId).toPromise().then((response: any) => {
            this.communicationModel = response.result.communicationModel;
            this.businessProfileSelectItems = response.result.businessProfileSelectItems;
            this.communicationForSelectItems = response.result.communicationForSelectItems;
            this.communicationMathodSelectItems = response.result.communicationMathodSelectItems;
            this.communicationStatusSelectItems = response.result.communicationStatusSelectItems;
            this.entityTypeId = response.result.entityTypeId;

            if (this.communicationId > 0) {

                this.communicationEntitySelectionBox.setValue(response.result.communicationModel.entityId);

                if (this.communicationModel.followupByContactId > 0) {
                    this.followupByContactSelectionBox.setValue(this.communicationModel.followupByContactId);
                }

            }
            this.fileUploadControl.setFileDataSourceByEntity(this.entityTypeId, this.communicationId);
            this.titlebar.initializeToolbar(this.communicationId == 0 ? "Notes & Comms : New" : "Notes & Comms: " + this.communicationModel.subject, null, this.toolbarType);
        });


    }

    onCommunicationForValueChanged(e): void {
        this.entityClass = this.hideEntityClass;

        if (this.communicationModel.businessProfileId > 0 && this.communicationModel.entityTypeId > 0) {
            this.entityClass = this.showEntityClass;
            this.communicationEntitySelectionBox.setEntityDataSource(this.communicationModel.entityTypeId, this.communicationModel.businessProfileId);
        }
       
        if (this.communicationModel.businessProfileId > 0 && this.communicationModel.isFollowupEnable) {
            this.followupByContactSelectionBox.setContactDataSource(this.communicationModel.businessProfileId);
        }

    }

    onFollowupEnableValueChanged(e): void {

        if (e.value) {
            this.followupCssClass = this.showFollowupCssClass;

            if (this.communicationModel.businessProfileId > 0) {
                this.followupByContactSelectionBox.isRequired = true;
                this.followupByContactSelectionBox.setContactDataSource(this.communicationModel.businessProfileId);
            }
        }
        else {
            this.followupCssClass = this.hideFollowupCssClass;
        }


    }

    attachValidationToControl() {
        this.subjecctValidation.validationRules = [{ type: 'required', message: 'Subject is required.' }];
        this.communicationDateTimeValidation.validationRules = [{ type: 'required', message: 'Subject is required.' }];
        this.businessProfileSelectionBoxValidation.validationRules = [{ type: 'required', message: 'Business Profile is required.' }];

    }

    saveEntity(action: DetailPageAction): void {
        this.communicationModel.entityId = this.communicationEntitySelectionBox.getValue();

        if (this.communicationModel.isFollowupEnable) {
            this.communicationModel.followupByContactId = this.followupByContactSelectionBox.getValue();
        }
        else {
            this.communicationModel.followupDate = null;
            this.communicationModel.followupByContactId = null;
        }

        if (this.communicationId == 0) {
            this.communicationService.createCommnunication(this.communicationModel).subscribe(data => {
                this.messageService.success("Record has been saved successfully", 'Information');

                this.communicationId = data.result;
                this.communicationModel.id = this.communicationId;
                this.redirectToListPage(action);

                this.fileUploadControl.saveEntityFiles(this.entityTypeId, this.communicationId);
            });
        }
        else {
            this.communicationService.updateCommnunication(this.communicationId, this.communicationModel).subscribe(data => {
                this.messageService.success("Record has been updated successfully", 'Information');
                this.communicationId = data.result;
                this.redirectToListPage(action);
                this.fileUploadControl.saveEntityFiles(this.entityTypeId, this.communicationId);
                this.fileUploadControl.setFileDataSourceByEntity(this.entityTypeId, this.communicationId);
            });
        }
    }

    redirectToListPage(action: DetailPageAction): void {



        var newNavigationUrl = '/communication/communication';

        if (action == DetailPageAction.Close || action == DetailPageAction.SaveAndClose) {

            this.navigationService.navigateToReturnurl(this.router.url);
        }
        else if (action == DetailPageAction.SaveAndNew) {

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl, this.router.url);
        }
        else if (action == DetailPageAction.Save && newNavigationUrl == this.router.url) {

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl + '/' + this.communicationId, this.router.url);
        }

    }
    validateAndSave(action: DetailPageAction): void {

        if (!this.formValidation.instance.validate().isValid) {
            return;
        }

        this.saveEntity(action);
    }

    onSaveClicked(e): void {

        this.validateAndSave(DetailPageAction.Save);
    }

    onSaveNNewClicked(): void {

        this.validateAndSave(DetailPageAction.SaveAndNew);
    }


    onSaveNCloseClicked(): void {

        this.validateAndSave(DetailPageAction.SaveAndClose);
    }

    onCloseClicked(): void {
        this.redirectToListPage(DetailPageAction.Close);
    }
}
