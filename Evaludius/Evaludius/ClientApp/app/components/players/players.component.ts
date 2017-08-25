// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { PlayersService } from "../../services/players.service";
import { AlertService, DialogType, MessageSeverity } from '../../services/alert.service';
import { AppTranslationService } from "../../services/app-translation.service";
import { AccountService } from "../../services/account.service";
import { Utilities } from "../../services/utilities";
import { Player } from '../../models/Player.model';
import { Team } from '../../models/team.model';
import { PlayerPosition } from '../../models/position.model';

import { PlayerInfoComponent } from "./player-info.component";




@Component({
    selector: 'players',
    templateUrl: './players.component.html',
    styleUrls: ['./players.component.css'],
    animations: [fadeInOut]
})
export class PlayersComponent implements OnInit {
       

    columns: any[] = [];
    rows: Player[] = [];
    rowsCache: Player[] = [];



    editedPlayer: Player;
    sourcePlayer: Player;
   
    loadingIndicator: boolean;

    allTeams: Team[] = [];
    allPositions: PlayerPosition[] = [];


    @ViewChild('indexTemplate')
    indexTemplate: TemplateRef<any>;

    
    @ViewChild('teamsTemplate')
    teamsTemplate: TemplateRef<any>;

    @ViewChild('actionsTemplate')
    actionsTemplate: TemplateRef<any>;

    @ViewChild('editorModal')
    editorModal: ModalDirective;

    @ViewChild('playerEditor')
    playerEditor: PlayerInfoComponent;

    
  
    constructor(private playersService: PlayersService, private translationService: AppTranslationService, private accountService: AccountService, private alertService: AlertService)
    {

    }

		

        ngOnInit(): void {

            let gT = (key: string) => this.translationService.getTranslation(key);

            this.columns = [
                { prop: "index", name: '#', width: 40, cellTemplate: this.indexTemplate, canAutoResize: false },
                { prop: 'firstName', name: gT('common.FirstName'), width: 100 },
                { prop: 'lastName', name: gT('common.LastName'), width: 100 },
                { prop: 'yearOfBirth', name: gT('players.management.YearOfBirth'), width: 50 },
                { prop: 'position', name: gT('players.management.Position'), width: 100 },

                { prop: 'teams', name: gT('players.management.Teams'), width: 120, cellTemplate: this.teamsTemplate }
            ];

            if (this.canManagePlayers)
                this.columns.push({ name: '', width: 130, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

            this.loadData();
			
    }

        loadData() {
            this.alertService.startLoadingMessage();
            this.loadingIndicator = true;
            this.playersService.getPlayers().subscribe(players => this.onDataLoadSuccessful(players, [],[]), error => this.onDataLoadFailed(error));
            
        }


        onDataLoadSuccessful(players: Player[], teams: Team[],positions: PlayerPosition[]) {
            this.alertService.stopLoadingMessage();
            this.loadingIndicator = false;

            players.forEach((player, index, players) => {
                (<any>player).index = index + 1;
            });

            this.rowsCache = [...players];
            this.rows = players;
            this.allTeams = teams;
            this.allPositions = positions;
          
        }

        onDataLoadFailed(error: any) {
            this.alertService.stopLoadingMessage();
            this.loadingIndicator = false;

            this.alertService.showStickyMessage("Load Error", `Unable to retrieve Players from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
                MessageSeverity.error, error);
        }

        onSearchChanged(value: string) {
            if (value) {
                value = value.toLowerCase();

                let filteredRows = this.rowsCache.filter(r => {
                    let isChosen = !value
                        || r.firstName.toLowerCase().indexOf(value) !== -1
                        || r.teams.some(i => i.toLowerCase().indexOf(value) !== -1)
                        || r.position.toLowerCase().indexOf(value) !== -1
                       
                    return isChosen;
                });

                this.rows = filteredRows;
            }
            else {
                this.rows = [...this.rowsCache];
            }
        }

        onEditorModalHidden() {
          
            this.playerEditor.resetForm(true);
        }


        newPlayer() {
           
            this.sourcePlayer = null;
            this.editedPlayer = this.playerEditor.newPlayer();
            this.editorModal.show();
        }


        editPlayer(row: Player) {
           
            this.sourcePlayer = row;
            this.editedPlayer = this.playerEditor.editPlayer(row);
            this.editorModal.show();
        }

        deletePlayer(row: Player) {
            this.alertService.showDialog('Are you sure you want to delete \"' + row.fullName + '\"?', DialogType.confirm, () => this.deletePlayerHelper(row));
        }


        deletePlayerHelper(row: Player) {

            this.alertService.startLoadingMessage("Deleting...");
            this.loadingIndicator = true;

            this.playersService.deletePlayer(row.id)
                .subscribe(results => {
                    this.alertService.stopLoadingMessage();
                    this.loadingIndicator = false;

                    this.rowsCache = this.rowsCache.filter(item => item !== row)
                    this.rows = this.rows.filter(item => item !== row)
                },
                error => {
                    this.alertService.stopLoadingMessage();
                    this.loadingIndicator = false;

                    this.alertService.showStickyMessage("Delete Error", `An error occured whilst deleting the Player.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
                        MessageSeverity.error, error);
                });
        }




        get canManagePlayers() {
            return this.accountService.userHasPermission(Permission.managePlayersPermission);
        }

}
