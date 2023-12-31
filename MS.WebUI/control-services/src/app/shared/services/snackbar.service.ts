import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { error } from "src/app/core/api";

@Injectable()
export class SnackBarService {
    constructor(protected snackBar: MatSnackBar) { }

    public ShowError(message: string, durationInSeconds?: number) {
        this.snackBar.open(message, '', {
            horizontalPosition: 'right',
            verticalPosition: 'top',
            duration: (durationInSeconds ?? 1000) * 1000,
            panelClass: ['snack-background-red']
          });
    }

    public ShowErrors(messages: error[], durationInSeconds?: number) {
        if (messages) {
            messages.forEach(error => {
                this.snackBar.open(error.message, '', {
                    horizontalPosition: 'right',
                    verticalPosition: 'top',
                    duration: (durationInSeconds ?? 5) * 1000,
                    panelClass: ['snack-background-red'],
                });
            });
        } else {
            this.snackBar.open("Não foi possível se comunicar ao servidor!", "", {
                horizontalPosition: 'right',
                verticalPosition: 'top',
                duration: (durationInSeconds ?? 5) * 1000,
                panelClass: ['snack-background-red']
            });
        }
    }

    public ShowSucess(message: string, durationInSeconds?: number) {
        this.snackBar.open(message, "", {
            horizontalPosition: 'right',
            verticalPosition: 'top',
            duration: (durationInSeconds ?? 5) * 1000,
            panelClass: ['snack-background-green']
        });
    }
}
