Răspunsuri ex 4:
- LineLoop desenează un poligon închis, conectând ultimul punct cu primul.
- LineStrip desenează un poligon deschis, conectând fiecare punct cu următorul.
- TriangleFan desenează un poligon închis, conectând primul punct cu fiecare pereche de puncte consecutive.
- TriangleStrip desenează un poligon deschis, conectând fiecare pereche de puncte consecutive.

Răspunsuri ex 7:
- Un gradient de culoare reprezintă o tranziție de culoare între două sau mai multe culori.
- Pentru a obține un gradient de culoare în OpenGL, se folosește funcția glShadeModel(GL_SMOOTH) pentru a activa interpolarea culorilor între vârfurile primitivei desenate.
- Se specifică culorile vârfurilor primitivei folosind funcția glColor3f sau glColor4f.
- OpenGL va interpola culorile între vârfuri, rezultând un gradient de culoare.


Răspuns ex 10:

Dacă se folosesc culori diferite pentru fiecare vârf al unei primitive desenate în modul strip, OpenGL va interpola culorile între vârfuri, rezultând un gradient de culoare pe întreaga lungime a primitivei.