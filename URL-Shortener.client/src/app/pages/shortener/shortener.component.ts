import { Component } from '@angular/core';
import { MainComponent } from '../../layouts/main/main.component';

@Component({
  selector: 'app-shortener',
  standalone: true,
  imports: [MainComponent],
  templateUrl: './shortener.component.html',
  styleUrl: './shortener.component.css'
})
export class ShortenerComponent {

}
