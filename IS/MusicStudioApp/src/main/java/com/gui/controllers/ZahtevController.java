package com.gui.controllers;


import java.util.List;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import javax.ejb.EJB;
import javax.faces.bean.ManagedBean;

import com.ejb.services.ZahtevService;
import com.jpa.entities.Zahtev;
import com.jpa.entities.Instrument;

@ManagedBean
public class ZahtevController {
	private Zahtev zahtev = new Zahtev();
	private Date vreme;
	private String primljenoVreme = "11/11/2000";

	private List<Integer> instrumenti;
	private int studioId;
	private int salaId;
	private double cena;
	private int brojPesama;
	private Boolean usluge;
	private int id;
	
	public String getPrimljenoVreme() {
		return primljenoVreme;
	}
	public void setPrimljenoVreme(String primljenoVreme) {
		this.primljenoVreme = primljenoVreme;
	}
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public Date getVreme() {
		return vreme;
	}
	public void setVreme(Date vreme) {
		this.vreme = vreme;
	}
	public List<Integer> getInstrumenti() {
		return instrumenti;
	}
	public void setInstrumenti(List<Integer> instrumenti) {
		this.instrumenti = instrumenti;
	}
	public int getStudioId() {
		return studioId;
	}
	public void setStudioId(int studioId) {
		this.studioId = studioId;
	}
	public int getSalaId() {
		return salaId;
	}
	public void setSalaId(int salaId) {
		this.salaId = salaId;
	}
	public double getCena() {
		return cena;
	}
	public void setCena(double cena) {
		this.cena = cena;
	}
	public int getBrojPesama() {
		return brojPesama;
	}
	public void setBrojPesama(int brojPesama) {
		this.brojPesama = brojPesama;
	}
	public Boolean getUsluge() {
		return usluge;
	}
	public void setUsluge(Boolean usluge) {
		this.usluge = usluge;
	}
	
	@EJB
	private ZahtevService service;
	
	public void saveZahtev() {
		try {
			SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy");
			vreme = sdf.parse(primljenoVreme);
		} catch (ParseException e) {
			e.printStackTrace();
		}
		zahtev = new Zahtev(vreme, studioId, salaId, brojPesama, cena, usluge);
		service.dodajZahtev(zahtev, instrumenti);
	}
	
	public void deleteZahtev() {
		service.ukloniZahtev(id);
	}
	
	public Zahtev findZahtev(int id) {
		return service.nadjiZahtev(id);
	}
	
	public List<Instrument> findInstruments(int id){
		List<Instrument> lista = service.nadjiInstrumente(id);
		if (lista == null || lista.isEmpty())
			return null;
		return lista;
	}
	
	public void updateZahtev() {
		service.azurirajZahtev(id, studioId, salaId, brojPesama, cena, usluge);
	}
	
	public List<Zahtev> allZahtev(){
		List<Zahtev> lista  = service.sviZahtevi();
		if (lista == null || lista.isEmpty())
			return null;
		return lista;
	}
	
	public double totalCena() {
		cena = service.ukupnaCena(zahtev.getId());
		return cena;
	}
}
