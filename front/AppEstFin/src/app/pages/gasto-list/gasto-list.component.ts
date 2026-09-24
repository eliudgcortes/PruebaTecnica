import { Component } from '@angular/core';
import { GastoService } from '../../servicios/gasto.service';

@Component({
  selector: 'app-gasto-list',
  standalone: true,
  templateUrl: './gasto-list.component.html'
})
export class GastoListComponent {

  // Lista de gastos.
  gastos: any[] = [];

  constructor(
    private gastoService: GastoService
  ) {}

  // Se ejecuta al iniciar.
  ngOnInit(): void {
    this.gastoService.obtenerGastos();
  }

  // Muestra el detalle del gasto.
  verDetalle(id: number) {

  }
}
