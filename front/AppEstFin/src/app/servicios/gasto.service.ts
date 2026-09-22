import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})

export class GastoService {

    private apiUrl ='https://localhost/api/Estimacion';

    constructor(
        private http: HttpClient
    ) { }


    obtenerGastos() {
        return this.http.get(`${this.apiUrl}/ConsultarConsumo`);
    }


    obtenerDetalle(id: number) {
        return this.http.get(`${this.apiUrl}/ObtenerGasto`);
    }
}
