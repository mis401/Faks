package com.ejb.services.impl;

import com.jpa.entities.Sala;
import com.ejb.services.SalaService;

import java.util.List;
import java.util.ArrayList;
import java.util.HashSet;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import javax.persistence.PersistenceContext;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
public class SalaServiceImplementation implements SalaService {
	
	private EntityManager em;
	
	public SalaServiceImplementation() 
		{
			EntityManagerFactory emf = Persistence.createEntityManagerFactory("MusicStudioPers");
			em = emf.createEntityManager();
	    }
	
	
	@Override
	public void dodajSalu(Sala s) {
		try {
		em.getTransaction().begin();
		em.persist(s);
		em.getTransaction().commit();
		}
		catch(Exception e) {
			e.printStackTrace();
		}
	}
	@Override
	public void ukloniSalu(int id) {
		try {
			Sala sala = em.find(Sala.class, id);
			if (sala == null)
				throw new Exception("Ne postoji sala sa tim id");
			em.getTransaction().begin();
			em.remove(sala);
			em.getTransaction().commit();
			}
		catch(Exception e) {
			e.printStackTrace();
		}
	}
	@Override
	public void azurirajSalu(int id, int studio, double cena) {
		try {
			Sala sala = em.find(Sala.class, id);
			if (sala == null)
				throw new Exception("Ne postoji sala sa tim id");
			em.getTransaction().begin();
			sala.setStudioId(studio);
			sala.setCena(cena);
			em.getTransaction().commit();
			}
		catch(Exception e) {
			e.printStackTrace();
		}
	}
	@Override
	public Sala nadjiSalu(int id) {
		try {
			Sala sala = em.find(Sala.class, id);
			if (sala == null)
				throw new Exception("Ne postoji sala sa tim id");
			return sala;
			}
		catch(Exception e) {
			e.printStackTrace();
		}
		return null;
	}
	@Override
	public List<Sala> sveSale(){
		List<Sala> sale = em.createQuery("SELECT s FROM Sala s", Sala.class).getResultList();
		return sale;
	}
}
