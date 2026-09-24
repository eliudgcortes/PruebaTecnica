import { Routes } from '@angular/router';
import { GastoListComponent } from './pages/gasto-list/gasto-list.component';
import { GastoDetailComponent } from './pages/gasto-detail/gasto-detail.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'gastos',
    pathMatch: 'full'
  },
  {
    path: 'gastos',
    component: GastoListComponent
  },
  {
    // Se agrega :id para recibir el ID del gasto.
    path: 'gasto/:id',
    component: GastoDetailComponent
  }
];
