package com.ejb.services.impl;

import java.util.List;
import java.util.ArrayList;
import java.util.HashSet;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import javax.persistence.PersistenceContext;
import com.ejb.services.InstrumentService;
import com.jpa.entities.Instrument;
@Stateless
public class InstrumentServiceImplementation implements InstrumentService {
	@PersistenceContext(name = "MusicStudioPers")
	private EntityManager em;

	/*
	 * public InstrumentServiceImplementation() { EntityManagerFactory emf =
	 * Persistence.createEntityManagerFactory("MusicStudioPers"); em =
	 * emf.createEntityManager(); }
	 */
	@Override
	public void dodajInstrument(Instrument ins) {
		try {
			//em.getTransaction().begin();
			em.persist(ins);
			//em.getTransaction().commit();
		}
		catch(Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public void ukloniInstrument(int id) {
		try {
		Instrument ins = em.find(Instrument.class, id);
		if (ins == null)
			throw new Exception("Ne postoji instrument sa tim id");
		//em.getTransaction().begin();
		em.remove(ins);
		//em.getTransaction().commit();
		}
		catch (Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public void azurirajInstrument(int id, String naziv, int studio, double cena) {
		try {
			Instrument ins = em.find(Instrument.class, id);
			if (ins == null)
				throw new Exception("Ne postoji instrument sa tim id");
			//em.getTransaction().begin();
			ins.setNaziv(naziv);
			ins.setStudioId(studio);
			ins.setCena(cena);
			//em.getTransaction().commit();
		}
		catch(Exception e) {
			e.printStackTrace();
		}

	}

	@Override
	public List<Instrument> sviInstrumenti() {
		
		List<Instrument> ins = em.createQuery("SELECT i FROM Instrument i", Instrument.class).getResultList();
		if (ins == null)
			return null;
        return ins;
	}
	
	@Override
	public Instrument nadjiInstrument(int id) {
		Instrument ins = em.find(Instrument.class, id);
		return ins;
	}
}
