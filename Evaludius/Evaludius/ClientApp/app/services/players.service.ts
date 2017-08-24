
import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/observable/forkJoin';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { PlayersEndpoint } from './players-endpoint.service';
import { Player } from '../models/player.model';

@Injectable()
export class PlayersService{

	constructor(private router: Router, private http: Http, 
		private playersEndpoint: PlayersEndpoint) {

    }


    updatePlayer(player: Player) {       

            return this.playersEndpoint.getUpdatePlayerEndpoint(player, player.id);     
        
    }


    newPlayer(player: Player) {
        return this.playersEndpoint.getNewPlayerEndpoint(player)
            .map((response: Response) => <Player>response.json());
    }

    deletePlayer(playerId?: number) {

        return this.playersEndpoint.deletePlayerEndpoint(playerId)
            .map((response: Response) => <Player>response.json());
    }

    getPlayer(playerId?: number) {

        return this.playersEndpoint.getPlayerEndpoint(playerId)
            .map((response: Response) => <Player>response.json());
    }
	
	getPlayers(page?: number, pageSize?: number) {

		return this.playersEndpoint.getPlayersEndpoint(page, pageSize)
			.map((response: Response) => <Player[]>response.json());
	}
}