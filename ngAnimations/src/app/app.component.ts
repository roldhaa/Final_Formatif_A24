import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shakeX, tada } from 'ng-animate';
import { timer } from 'rxjs';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';


const BOUNCE_DURATION_SECONDS = 2.0;
const SHAKE_DURATION_SECONDS = 4.0;
const TADA_DURATION_SECONDS = 3.0;
@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    standalone: true,
     animations: [
    trigger('bounce', [transition(':increment', useAnimation(bounce, {params: {timing: BOUNCE_DURATION_SECONDS}}))]),
    trigger('shake', [transition(':increment', useAnimation(shakeX, {params: {timing: SHAKE_DURATION_SECONDS}}))]),
    trigger('tada', [transition(':increment', useAnimation(tada, {params: {timing: TADA_DURATION_SECONDS}}))]),
  ]
})
export class AppComponent {
  title = 'ngAnimations';

  ng_bounce = 0;
  ng_shake = 0;
  ng_tada = 0;

  constructor() {

  }
  async waitFor(delayInSeconds:number) {
  await lastValueFrom(timer(delayInSeconds * 1000));
}

async Animationunefois(){

   this.ng_shake++

  await lastValueFrom(timer(SHAKE_DURATION_SECONDS * 1000));
  // Après 2 seconde
  this.ng_bounce++;

  await lastValueFrom(timer(BOUNCE_DURATION_SECONDS * 1000));
  // Après 4 secondes
this.ng_tada++
  }

  lancerAnimations(boucle: boolean) {
  ... exécute les 3 animations ...
  if (boucle === true) {
     ... recommence la séquence ...
  }
}


}
