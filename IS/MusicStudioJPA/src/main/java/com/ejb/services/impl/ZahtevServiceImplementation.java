package com.ejb.services.impl;

import com.ejb.services.ZahtevService;
import com.jpa.entities.Instrument;
import com.jpa.entities.Zahtev;
import com.jpa.entities.ZahtevInstrument;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashSet;
import java.util.List;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import javax.persistence.PersistenceContext;

public class ZahtevServiceImplementation implements ZahtevService {

	private EntityManager em;
	
	public ZahtevServiceImplementation() {
		EntityManagerFactory emf = Persistence.createEntityManagerFactory("MusicStudioPers");
		em = emf.createEntityManager();
	}
	
	@Override
	public void dodajZahtev(Zahtev z, List<Integer> instrumenti) {
		try {
		List<ZahtevInstrument> listaInstrumenata = new ArrayList<ZahtevInstrument>();
		em.getTransaction().begin();
		em.persist(z);
		em.getTransaction().commit();
		
		em.getTransaction().begin();
		for(int i : instrumenti){
			System.out.println(i);
			Instrument ins = em.find(Instrument.class, i);
			if (ins == null)
				throw new Exception("Nema tog instrumenta");
			ZahtevInstrument zi = new ZahtevInstrument(ins.getId(), z.getId());
			listaInstrumenata.add(zi);
		}
		for(ZahtevInstrument zi : listaInstrumenata) {
			em.persist(zi);
		}
		em.getTransaction().commit();
		}
		catch(Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public void ukloniZahtev(int id) {
		try {
			Zahtev zahtev = em.find(Zahtev.class, id);
			if (zahtev == null)
				throw new Exception("Ne postoji zahtev sa tim id");
			List<ZahtevInstrument> listZI = em
					.createQuery("SELECT z FROM ZahtevInstrument z WHERE z.zahtevId = :id", ZahtevInstrument.class)
					.setParameter("id", zahtev.getId()).getResultList();
			
			em.getTransaction().begin();
			for(ZahtevInstrument zi : listZI) {
				em.remove(zi);
			}
			em.remove(zahtev);
			em.getTransaction().commit();
		}
		catch(Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public void azurirajZahtev(int id, int studioId, int salaId, int brojPesama, double cena, boolean usluge) {
		try {
		Zahtev zahtev = em.find(Zahtev.class, id);
		if (zahtev == null)
			throw new Exception("Ne postoji zahtev sa tim id");
		
		em.getTransaction().begin();
		zahtev.setBrojPesama(brojPesama);
		zahtev.setCena(cena);
		zahtev.setSalaId(salaId);
		zahtev.setUsluge(usluge);
		em.getTransaction().commit();
		
		}
		catch(Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public Zahtev nadjiZahtev(int id) {
		try {
			Zahtev zahtev = em.find(Zahtev.class, id);
			if (zahtev == null)
				throw new Exception("Nema tog zahteva");
			return zahtev;
		}
		catch(Exception e) {
			e.printStackTrace();
		}
		return null;
	}
	
	@Override
	public List<Instrument> nadjiInstrumente(int id) {
		List<Instrument> lista = new ArrayList<Instrument>();
		try {
			Zahtev zahtev = em.find(Zahtev.class, id);
			if (zahtev == null)
				throw new Exception("Nema tog zahteva");
			List<ZahtevInstrument> listaZI = em
					.createQuery("SELECT zi FROM ZahtevInstrument zi WHERE zi.zahtevId = :zid", ZahtevInstrument.class)
					.setParameter("zid", zahtev.getId())
					.getResultList();
			for(ZahtevInstrument zi : listaZI) {
				Instrument ins = em.find(Instrument.class, zi.getInstrumentId());
				lista.add(ins);
			}
			return lista;
		}
		catch(Exception e) {
			e.printStackTrace();
		}
		return null;
	}
	
	@Override
	public void ocistiTabeluZahtev() {
		List<Zahtev> lista = em.createQuery("SELECT z FROM Zahtev z", Zahtev.class).getResultList();
		for(Zahtev z:lista) {
			ukloniZahtev(z.getId());
		}
	}
	
	@Override
	public List<Zahtev> sviZahtevi(){
		List<Zahtev> lista = em.createQuery("SELECT z FROM Zahtev z", Zahtev.class).getResultList();
		return lista;
	}
}
