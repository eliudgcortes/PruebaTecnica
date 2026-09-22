import { Component } from '@angular/core';
import { GastoService } from '../../servicios/gasto.service';

@Component({
  selector: 'app-gasto-list',
  standalone: true,
  templateUrl: './gasto-list.component.html'
})

export class GastoListComponent {
  gastos: any[] = [];

  constructor(
    private gastoService: GastoService
  ) {}


  ngOnInit(): void {
    this.gastoService.obtenerGastos();
  }


  verDetalle(id: number) {

  }

}