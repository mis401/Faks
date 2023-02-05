package com.ejb.services;

import java.util.Date;
import java.util.List;


import com.jpa.entities.Zahtev;
import com.jpa.entities.Instrument;

public interface ZahtevService {
	public void dodajZahtev(Zahtev z, List<Integer> instrumenti);
	public void ukloniZahtev(int id);
	public void azurirajZahtev(int id, int studioId, int salaId, int brojPesama, double cena, boolean usluge);
	public Zahtev nadjiZahtev(int id);
	public List<Instrument> nadjiInstrumente(int id);
	public void ocistiTabeluZahtev();
	public List<Zahtev> sviZahtevi();
	public double ukupnaCena(int id);
}
