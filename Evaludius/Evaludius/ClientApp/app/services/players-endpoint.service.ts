// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

import { Injectable, Injector } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { EndpointFactory } from './endpoint-factory.service';
import { ConfigurationService } from './configuration.service';


@Injectable()
export class PlayersEndpoint extends EndpointFactory {

	private readonly _playersUrl: string = "/api/evaludius/players";
    private readonly _playerUrl: string = "/api/evaludius/player";

	get playersUrl() { return this.configurations.baseUrl + this._playersUrl; }
    get playerUrl() { return this.configurations.baseUrl + this._playerUrl; }



	constructor(http: Http, configurations: ConfigurationService, injector: Injector) {

		super(http, configurations, injector);
    }



    getNewPlayerEndpoint(playerObject: any): Observable<Response> {

        return this.http.post(this.playersUrl, JSON.stringify(playerObject), this.getAuthHeader(true))
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getNewPlayerEndpoint(playerObject));
            });
    }

    getUpdatePlayerEndpoint(playerObject: any, playerId?: number): Observable<Response> {
        let endpointUrl = playerId ? `${this.playersUrl}/${playerId}` : this.playerUrl;

        return this.http.put(endpointUrl, JSON.stringify(playerObject), this.getAuthHeader(true))
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getUpdatePlayerEndpoint(playerObject, playerId));
            });
    }

    deletePlayerEndpoint(playerId?: number): Observable<Response> {
        let endpointUrl = playerId ? `${this.playersUrl}/${playerId}` : this.playerUrl;

        return this.http.delete(endpointUrl, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getPlayerEndpoint(playerId));
            });
    }

    getPlayerEndpoint(playerId?: number): Observable<Response> {
        let endpointUrl = playerId ? `${this.playersUrl}/${playerId}` : this.playerUrl;

        return this.http.get(endpointUrl, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getPlayerEndpoint(playerId));
            });
    }

	getPlayersEndpoint(page?: number, pageSize?: number): Observable<Response> {
		let endpointUrl = page && pageSize ? `${this.playersUrl}/${page}/${pageSize}` : this.playersUrl;

		return this.http.get(endpointUrl, this.getAuthHeader())
			.map((response: Response) => {
				return response;
			})
			.catch(error => {
				return this.handleError(error, () => this.getPlayersEndpoint(page, pageSize));
			});
	}


}