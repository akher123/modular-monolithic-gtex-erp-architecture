import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { ApplicationMenuService } from 'app/application/application-shared/services/application-menu.service';
import { TitlebarComponent } from 'app/application/application-shared/components/titlebar/titlebar.component';
import { DxValidationGroupComponent, DxValidatorComponent } from 'devextreme-angular';
import { ToolbarType, DetailPageAction } from 'app/application/application-shared/components/titlebar/utilities';
import { MessageService } from 'app/shared/services/message.service';
import { NavigationService } from 'app/shared/services/navigation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TreeViewDropDownBoxComponent } from '../../../application-shared/components/common/app-tree-view-drop-down-box.component';

@Component({
  selector: 'app-applicantion-menu',
  templateUrl: './applicantion-menu.component.html',
  styleUrls: ['./applicantion-menu.component.scss']
})
/** applicantion-menue component*/
export class ApplicantionMenuComponent implements OnInit, AfterViewInit {

  applicationMenuDataSource: any = [];
  formData: any = {};
  id: any = 0;
  currentItem: any = {};
  disabledInUpdateMode = false;
  contentClass: any = "detail-page-content-div";
  @ViewChild(TitlebarComponent)
  private titlebar: TitlebarComponent;

  @ViewChild('formValidation')
  private formValidation: DxValidationGroupComponent;

  @ViewChild('menuNameValidation')
  private menuNameValidation: DxValidatorComponent;
  @ViewChild('captionValidation')
  private captionValidation: DxValidatorComponent;
  @ViewChild('parentMenu')
  parentMenu: TreeViewDropDownBoxComponent

  toolbarType: ToolbarType = ToolbarType.DetailPage;

  constructor(private applicationMenuService: ApplicationMenuService,
    private messageService: MessageService,
    private navigationService: NavigationService,
    private route: ActivatedRoute,
    private router: Router) {

  }
  ngOnInit(): void {
    this.init();
    this.attachValidationToControl();
  }

  ngAfterViewInit(): void {
    this.titlebar.initializeToolbar("Applicantion Menu", null, this.toolbarType);
  }
  init() {
    this.applicationMenuService.getCurrentUserAppMenu().subscribe(data => {
      this.applicationMenuDataSource = data.result.applicationMenu;
    });

    this.parentMenu.clearDatasource();

    this.parentMenu.setDatasource(this.applicationMenuService.getApplicationTeeList());
  }

  attachValidationToControl() {

    //validation
    this.menuNameValidation.validationRules = [{ type: 'required', message: 'Menu Name is required.' }];
    this.captionValidation.validationRules = [{ type: 'required', message: 'Caption is required.' }];

  }

  selectItem(e) {
    this.currentItem = e.itemData;
    this.formData.name = this.currentItem.name;
    this.formData.caption = this.currentItem.caption;
    this.formData.rowNo = this.currentItem.rowNo;
    this.formData.navigateUrl = this.currentItem.navigateUrl;
    this.formData.imageSource = this.currentItem.imageSource;
    this.formData.entityRightId = this.currentItem.entityRightId;
    this.formData.isVisible = this.currentItem.isVisible;
    this.formData.id = this.currentItem.id;
    this.formData.parentId = this.currentItem.parentId;

  }



  saveEntity(action: DetailPageAction): void {
    this.applicationMenuService.createApplicationMenu(this.formData).subscribe(data => {
      this.messageService.success("Record has been saved successfully", 'Information');
      this.id = data.result;
      this.formData.id = this.id;
      this.init()

    });

    //else {
    //  this.applicationMenuService.updateApplicationMenu(this.id, this.formData).subscribe(data => {
    //    this.messageService.success("Record has been updated successfully", 'Information');
    //    this.id = data.result;
    //    this.redirectToListPage(action);
    //  });
    //}


  }

  onParentMenuSelectedValueChange(e) {
    if (e != null) {
      this.formData.parentId = e;
    }
  }

  redirectToListPage(action: DetailPageAction): void {
    this.navigationService.navigateToReturnurl(this.router.url);
  }

  /**
  * validate and save data
  */
  validateAndSave(action: DetailPageAction): void {

    if (!this.formValidation.instance.validate().isValid) {
      return;
    }

    this.saveEntity(action);
  }




  /**
   * on save button clicked
   */
  onSaveClicked(e): void {

    this.validateAndSave(DetailPageAction.Save);
  }

  /**
  * on close button clicked
  */
  onCloseClicked(e): void {
    this.redirectToListPage(DetailPageAction.Close);
  }

}
