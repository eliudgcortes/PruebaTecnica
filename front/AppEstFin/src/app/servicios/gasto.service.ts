import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class GastoService
{

    // URL de la API.
    private apiUrl = 'https://localhost/api/Estimacion';

    constructor
	(
        private http: HttpClient
    ) { }

    // Obtiene la lista de gastos.
    obtenerGastos()
	{
        return this.http.get(`${this.apiUrl}/ConsultarConsumo`);
    }

    // Obtiene el detalle de un gasto.
    obtenerDetalle(id: number)
	{
        // Se envia idGasto para hacer match con el controller, agregando ?idGasto=${id}.
        return this.http.get(`${this.apiUrl}/ObtenerGasto?idGasto=${id}`);
    }
}
