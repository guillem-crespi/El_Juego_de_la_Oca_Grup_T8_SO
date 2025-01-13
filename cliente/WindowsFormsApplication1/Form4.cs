using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//FORM4: CORRESPONDE A LAS INSTRUCCIONES EL JUEGO
/*
REGLAS DEL JUEGO DE LA OCA (Reglas del juego -> http://museodeljuego.org/wp-content/uploads/contenidos_0000000699_docu1.pdf)

El objetivo del juego es ser el primero en llegar a la casilla central (casilla 63) del tablero, 
saltando de posiciones, según la tirada de los dados y sometido a unas reglas de juego, 
establecidas para cada casilla. 

-Casilla con OCA: Se avanza hasta la siguiente casilla en la que hay una oca y se vuelve a tirar.

-Casilla 6 y 12 (PUENTE). Se salta a la casilla 19 (posada) y se pierde un turno.

-Casilla 19 (POSADA). Se pierde un turno. 

-Casilla 31 (POZO).  NO se puede volver a jugar hasta que no pase otro jugador por esa casilla. 

-Casilla 42 (LABERINTO). Se retrocede a la casilla 30. 

-Casilla 52 (CARCEL). Se pierde dos turnos.

-Casilla 58 (CALAVERA). Se retrocede a la casilla 1.

-Casilla 63 (FINAL). Es necesario el numero de dados exactos para entrar, en caso de exceso 
se retroceden tantas casillas como puntos sobrantes.
 */
namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
    }
}
