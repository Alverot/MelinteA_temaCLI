# MelinteA_temaCLI
1.Ce este un viewport?
Viewportul este fereastra pe care se realizeaza randarea in functie de dimensiunile acesteia si coordonatelepixelilor.

2.Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?
Frames per second reprezinta cat de des sunt generate si afisate cadrele intr-o secunda.

3.Când este rulată metoda OnUpdateFrame()?
Aceasta este rulata de fiecare data cand se actualizeaza starea aplicatie.

4.Ce este modul imediat de randare?
Este o tehnica de randare care trimite date direct catre GPU in timpul procesului de randare.

5.Care este ultima versiune de OpenGL care acceptă modul imediat?
Ultima versiune care accepta modul de randare imediat este 3.3.3

6.Când este rulată metoda OnRenderFrame()?
Este apelata de fiecare data cand trebuie randat ceva pe ecrana adica la fiecare frame.

7.De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
Este necesara executarea a cel putin o data a metodei OnResize() pentru a initializa viewportul si perspectiva. Aceasta trebuie reapelata daca se schimba dimensiunile ferestrei

8.Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?
CreatePerspectiveFieldOfView() este folosita pentru a ajusta ce se vede pe ecran.
Parametri sunt:
-fovy(field of view) intre 0 si pi/2 determina unghiul de viziune
-aspect raportul dintre inaltime si latime
-zNear cea mai apropiata distanta la care camera vede
-zFar cea mai mare distanta la care camera vede
