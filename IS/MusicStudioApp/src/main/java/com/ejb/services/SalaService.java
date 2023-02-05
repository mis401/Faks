package com.ejb.services;

import com.jpa.entities.Sala;
import java.util.List;
public interface SalaService {
	
	public void dodajSalu(Sala s);
	public void ukloniSalu(int id);
	public void azurirajSalu(int id, int brojSale, int studio, double cena);
	public Sala nadjiSalu(int id);
	public List<Sala> sveSale();
}
