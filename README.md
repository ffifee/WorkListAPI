# WorkListAPI

API je napravljen na .NET 5 verziji.
Kao sigurnost i autorizaciju koristi JWT.

API je razvijen kao backend koji omogućava korisnicima registraciju, login i kreiranje/editiranje "Work lista".

Mogućnosti:
1. Registrirati korisnički račun kao korisnik ili admin.
2. Ulogiravanje u aplikaciju, prilikom login-a korisnik dobije token (koristi HSA256 algoritam) i token vrijedi određeno vrijeme.
3. Token se koristi kao autentikacija između klijenta i servera.
4. Kreiranje Work listi
5. Editiranje Work listi
6. Brisanje work listi
7. Pregled svoje work liste

Dodatne sigurnosti:
Prilikom registracije, provjerava se ispravnost unesenih podataka (provjera ispravno napisanog maila)
Provjerava se ispravnost tokena, ukoliko nije ispravan token, prilikom pristupa "Work listi" korisnik dobije grešku 401 Not Authorized.
