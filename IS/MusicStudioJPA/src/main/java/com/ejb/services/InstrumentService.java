package com.ejb.services;

import java.util.List;

import com.jpa.entities.Instrument;

public interface InstrumentService {
	public void dodajInstrument(Instrument ins);
	public void ukloniInstrument(int id);
	public void azurirajInstrument(int id, String naziv, int studio, double cena);
	public Instrument nadjiInstrument(int id);
	public List<Instrument> sviInstrumenti();
}
