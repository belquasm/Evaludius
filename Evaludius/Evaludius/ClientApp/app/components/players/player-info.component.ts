// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

import { Component, OnInit, ViewChild, Input } from '@angular/core';

import { AlertService, MessageSeverity } from '../../services/alert.service';
import { AccountService } from "../../services/account.service";
import { PlayersService } from "../../services/players.service";

import { Utilities } from '../../services/utilities';
import { Player } from '../../models/player.model';

import { Role } from '../../models/role.model';
import { Permission } from '../../models/permission.model';


@Component({
    selector: 'player-info',
    templateUrl: './player-info.component.html',
    styleUrls: ['./player-info.component.css']
})


export class PlayerInfoComponent implements OnInit {

    private isEditMode = false;
    private isNewPlayer = false;
    private isSaving = false;
  
    
    private showValidationErrors = false;
    private formResetToggle = true;
   
    private uniqueId: string = Utilities.uniqueId();
    private player: Player = new Player();
    private playerEdit: Player;
    private allRoles: Role[] = [];

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    @Input()
    isViewOnly: boolean;

    @Input()
    isGeneralEditor = false;





    @ViewChild('f')
    private form;

    //ViewChilds Required because ngIf hides template variables from global scope
    @ViewChild('firstName')
    private firstName;

    @ViewChild('lastName')
    private lastName;

    @ViewChild('yearOfBirth')
    private yearOfBirth;

    @ViewChild('dateOfBirth')
    private dateOfBirth;

    @ViewChild('team')
    private team;

    @ViewChild('position')
    private position;

    


    constructor(private alertService: AlertService, private playersService: PlayersService, private accountService:AccountService) {
    }

    ngOnInit() {
        if (!this.isGeneralEditor) {
            this.loadCurrentPlayerData();
        }
    }



    private loadCurrentPlayerData() {
        this.alertService.startLoadingMessage();


        this.playersService.getPlayer().subscribe(player => this.onCurrentUserDataLoadSuccessful(player), error => this.onCurrentUserDataLoadFailed(error));
        
    }


    private onCurrentUserDataLoadSuccessful(player: Player) {
        this.alertService.stopLoadingMessage();
        this.player = player;
        
    }

    private onCurrentUserDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

        this.player = new Player();
    }



   



    private showErrorAlert(caption: string, message: string) {
        this.alertService.showMessage(caption, message, MessageSeverity.error);
    }


    


    private edit() {
        if (!this.isGeneralEditor) {
           
            this.playerEdit = new Player();
            Object.assign(this.playerEdit, this.player);
        }
        else {
            if (!this.playerEdit)
                this.playerEdit = new Player();

            
        }

        this.isEditMode = true;
        this.showValidationErrors = true;
       
    }


    private save() {
        this.isSaving = true;
        this.alertService.startLoadingMessage("Saving changes...");

        if (this.isNewPlayer) {
            this.playersService.newPlayer(this.playerEdit).subscribe(player => this.saveSuccessHelper(player), error => this.saveFailedHelper(error));
        }
        else {
            this.playersService.updatePlayer(this.playerEdit).subscribe(response => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
        }
    }


    private saveSuccessHelper(player?: Player) {

        if (player)
            Object.assign(this.playerEdit, player);

        this.isSaving = false;
        this.alertService.stopLoadingMessage();
       
        this.showValidationErrors = false;


        Object.assign(this.player, this.playerEdit);
        this.playerEdit = new Player();
        this.resetForm();


        if (this.isGeneralEditor) {
            if (this.isNewPlayer)
                this.alertService.showMessage("Success", `Player \"${this.player.fullName}\" was created successfully`, MessageSeverity.success);
            else
                this.alertService.showMessage("Success", `Changes to user \"${this.player.fullName}\" was saved successfully`, MessageSeverity.success);
        }

        

        this.isEditMode = false;


        if (this.changesSavedCallback)
            this.changesSavedCallback();
    }


    private saveFailedHelper(error: any) {
        this.isSaving = false;
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Save player Error", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }
    

    private cancel() {
        if (this.isGeneralEditor)
            this.playerEdit = this.player = new Player();
        else
            this.player = new Player();

        this.showValidationErrors = false;
        this.resetForm();

        this.alertService.showMessage("Cancelled", "Operation cancelled by user", MessageSeverity.default);
        this.alertService.resetStickyMessage();

        if (!this.isGeneralEditor)
            this.isEditMode = false;

        if (this.changesCancelledCallback)
            this.changesCancelledCallback();
    }


    private close() {
        this.playerEdit = this.player = new Player();
        this.showValidationErrors = false;
        this.resetForm();
        this.isEditMode = false;

        if (this.changesSavedCallback)
            this.changesSavedCallback();
    }

    
    resetForm(replace = false) {
      
        if (!replace) {
            this.form.reset();
        }
        else {
            this.formResetToggle = false;

            setTimeout(() => {
                this.formResetToggle = true;
            });
        }
    }


    newPlayer() {
        this.isGeneralEditor = true;
        this.isNewPlayer = true;        
        this.player = this.playerEdit = new Player();
        this.playerEdit.isActive = true;
        this.edit();

        return this.playerEdit;
    }

    editPlayer(player: Player) {
        if (player) {
            this.isGeneralEditor = true;
            this.isNewPlayer = false;

           

            this.player = new Player();
            this.playerEdit = new Player();
            Object.assign(this.player, player);
            Object.assign(this.playerEdit, player);
            this.edit();

            return this.playerEdit;
        }
        else {
            return this.newPlayer();
        }
    }


    //displayUser(user: User, allRoles?: Role[]) {

    //    this.user = new User();
    //    Object.assign(this.user, user);
    //    this.deletePasswordFromUser(this.user);
    //    this.setRoles(user, allRoles);

    //    this.isEditMode = false;
    //}



    



    
}
