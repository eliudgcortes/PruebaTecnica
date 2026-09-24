import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GastoService } from '../../servicios/gasto.service';

@Component({
  selector: 'app-gasto-detail',
  standalone: true,
  templateUrl: './gasto-detail.component.html'
})
export class GastoDetailComponent implements OnInit {

  // Gasto que se mostrara en la vista.
  gasto: any;

  constructor(
    private route: ActivatedRoute,
    private gastoService: GastoService
  ) {}

  // Se ejecuta al iniciar el componente.
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    // Muestra el ID recibido en la consola.
    console.log(id);
  }
}
