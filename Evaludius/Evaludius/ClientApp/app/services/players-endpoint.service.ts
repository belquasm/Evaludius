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


	get playersUrl() { return this.configurations.baseUrl + this._playersUrl; }




	constructor(http: Http, configurations: ConfigurationService, injector: Injector) {

		super(http, configurations, injector);
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