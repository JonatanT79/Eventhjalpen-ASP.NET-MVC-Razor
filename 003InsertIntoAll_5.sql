use TranbarDB 

insert into UserAdress(Adress,ZipCode,City) 
values
('Hornsgatan 92', '118 21','Stockholm'),
('Vallgatan 21', '411 16','G�teborg'),
('Ymersgata 3', '215 36','Malm�'),
('Lekgatan 7', '724 62','V�ster�s')

Insert into Users(Firstname, Lastname, Email, Phonenumber, UserAdressID)
Values 
('Sven', 'Svensson', 'Sven.Svensson@gmail.com', '070876568',3),
('Anders', 'Borg', 'Anders.Borg@gmail.com','0707604923',1),
('G�ran', 'Andersson', 'golle@gmail.com', '071212345',2),
('Eva','Lindgren', 'Eva.Lindgren@gmail.com', '070976557',4),
('Kalle', 'Berg', 'kalle121@gmail.com', '0702350455',1)

Insert into Products(ProductName, Description,Quantity,Price)
Values 
('Sm�r', 'Svenskt','500 g',49.90), --1
('�gg','','6 Pack', 17.90), --2
('Str�socker', '','1 Kg', 15.90), --3
('Kakao','','200 g', 30.90), --4
('Florsocker','','500 g', 17.90), --5
('Vispgr�dde','40%','3 dl', 18.90), --6
('Vetemj�l','','2 kg', 14.90), --7
('Florsocker','','500 g', 17.90), --8
('Potatismj�l','','500 g', 12.90), --9
('Bakpulver','','225 g', 14.90), --10

('Vaniljsocker','','170 g', 17.90), --11
('Jordgubbssaft','Drickf�rdig','50 cl', 41.90), --12
('Jordgubbssylt','Ekologisk','400 g', 28.90), --13
('Jordgubbar','','400 g', 39.90), --14
('Kokosgr�dde','','400 ml', 25.90), --15
('Salt','','150 g', 8.90), --16
('Svartpeppar','Grovmalen','41 g', 24.90), --17
('Curry','','34 g', 27.90),  --18
('Kycklingfil�','Frysta','1 g', 90.90),  --19
('Matolja','','500 ml', 25.90), --20

('Rotselleri','','450 g', 9.90),  --21
('Vitvinsvin�ger','','50 cl', 35.50),  --22
('Olivolja','','750 ml', 61.00), --23
('Vitl�k','3 Pack','100 g', 17.90), --24
('Schalottenl�k','','250 g', 18.90),  --25
('Ryggbiff','F�rsk',' 180 g', 72.90), --26
('Str�br�d', '', '400 g', 15.90), --27
('Majsst�rkelse','', '400 g', 18.90), --28
('Grillspett', 'Bambu', '100 st', 12.90), --29
('Koriander', 'Fryst', '40 g', 11.50), --30
('Kalvfond', 'Koncentrerad', '180 ml', 34.90), --31
('Vatten', '', '', 0.00) --32

Insert into RecipeType(RecipeTypeName)
Values 
('Bakverk'), --1
('F�rr�tt'), --2
('Middag'), --3
('Vegetarisk') --4

---�ndrade recipeTypeID
Insert into Recipe(RecipeTypeID, RecipeName, EstimatedTime)
Values 
(1,'Kladdkaka', 40), --1 
(1,'Jordgubbst�rta',50), --2
(4,'Kycklingspett', 70), --3
(4,'Ryggbiff med rotselleripur� och vitl�kssky', 60) --4

insert into MeasurementUnit(Measurement) 
values
('kg'), --1
('g'), --2
('mg'),  --3
('l'), --4
('dl'), --5
('cl'), --6
('ml'), --7
('msk'), --8
('tsk'), --9
('kryddm�tt'), --10
('st'), --11
('Klyftor'), --12
('Skivor') --13

Insert into RecipeDetails(RecipeID, ProductID, ProductQuantity, MeasurementUnitID)
Values 
--Kladdkaka
(1, 1, 100, 2),
(1, 3, 2.5, 5),
(1, 2, 2, 11),
(1, 7, 1, 5),
(1, 4, 3, 8),
(1, 11, 1, 9),
(1, 8, 0, 11),
(1, 7, 2, 5),

--Jordgubbst�rta
(2, 7, 1, 5),
(2, 9, 1, 9),
(2, 2, 4, 11), -- �GG
(2, 3, 2, 5), -- STR�SOCKER
(2, 1, 0.5, 8),
(2, 27, 1.5, 8),
(2, 6, 1.5, 5), --GR�DDE
(2, 2, 1, 11), -- OBS DETTA �R ETT �GG TILL!! --
(2, 3, 2, 9), -- OBS DETTA �R MER STR�SOCKER --
(2, 28, 1, 8),
(2, 11, 2, 9),
(2, 6, 1, 5), -- OBS DETTA �R MER GR�DDE, MEN DENNA G�NG VISPAD! --
(2, 12, 1, 5),
(2, 13, 2, 5),
(2, 6, 4, 5), -- GR�DDE IGEN--
(2, 14, 0, 0), --JORDUBBAR, OKLAR M�NGD

--Kycklingspett
(3, 15, 2, 5),
(3, 3, 1, 8),
(3, 16, 1, 9),
(3, 18, 2, 8),
(3, 19, 600, 2),
(3, 20, 0.5, 5),
(3, 29, 1, 11), -- DETTA �R PRODUKTEN GRILLSPETT AV TR� (INTE MAT ALLTS�)
(3, 30, 3, 8), 

--Ryggbiff
(4, 21, 1, 1),
(4, 22, 2, 8 ),
(4, 23, 0.5, 5),
(4, 32, 0, 11), --OKLAR M�NGD VATTEN
(4, 24, 3, 12), -- VITL�KSKLYFTOR, HUR R�KNA?
(4, 25, 1.5, 11),
(4, 1, 37.5, 2),
(4, 22, 0.75, 8),
(4, 31, 1, 8),
(4, 32, 2.5, 5),
(4, 26, 4, 13),
(4, 16, 1, 9),
(4, 20, 1, 8),
(4, 1, 12.5, 2)

Insert into Orders(UserID, SumToPay, CurrentDate)
Values 
(1, 365, GETDATE()),
(2, 165, GETDATE()),
(3, 233, GETDATE()),
(4, 321, GETDATE())

-- --------------------------

Insert into OrderDetails(OrdersID, ProductID)
Values 
(1, 5),
(2, 2),
(3, 3),
(4, 4)

Insert into [Events](EventType)
Values 
('Fest'),
('Kalas'),
('Br�llop'),
('Studentfest')

Insert into EventDetails(RecipeID, EventID)
Values 
(1, 4),
(2, 2),
(3, 1),
(4, 3)

Insert into RecipeSteps(RecipeID, Stepnumber,Instructions)
Values 
-- Kladdkaka
(1, 1, 'S�tt ugnen p� 175 grader'),
(1, 2, 'Sm�lt sm�ret i en kastrull. Lyft av kastrullen fr�n plattan'),
(1, 3, 'R�r ner socker och �gg, blanda v�l. R�r ner �vriga ingredienser s� att det blir v�l blandat'),
(1, 4, 'H�ll smeten i en smord och br�ad form med l�stagbar kant.'),
(1, 5, 'Gr�dda mitt i ugnen i cirka 15 min. Kakan blir l�g med ganska h�rd yta och lite kladdig i mitten'),
(1, 6, 'L�t kakan kallna, Pudra �ver florsocker. Servera med gr�dde eller glass och frukt'),

--Jordgubbst�rta
(2, 1, 'S�tt ugnen p� 175 grader. Sm�rj och br�a formen. Blanda vetemj�l, potatismj�l och bakpulver i en sk�l. Vispa �gg och socker s� att blandningen blir ljus och p�sig. Sockret ska ha l�st sig helt. Sikta ner mj�lblandningen i �ggsmeten och blanda.'),
(2, 2, 'H�ll upp i formen. Gr�dda mitt i ugnen ca 35 min.'),
(2, 3, 'Vaniljkr�m: Blanda gr�dde, �ggula, socker och majsst�rkelse i en kastrull. Sjud under omr�rning tills kr�men tjocknar. Ta fr�n v�rmen. Blanda i vaniljsockret. L�t kallna.'),
(2, 4, 'V�nd ner vispad gr�dde n�r det �r dags att l�gga ihop t�rtan.'),
(2, 5, 'Ihopl�ggning: Sk�r t�rtbottnen i tre delar. Pensla bottnarna med saft. Sk�r jordgubbarna i sm� bitar och blanda med vaniljkr�men.'),
(2, 6, 'Bred jordgubbssylt och d�refter vaniljkr�m p� de tv� understa bottnarna. L�gg p� den �versta bottnen. Plasta in t�rtan noga och l�t den safta till sig i kylen, g�rna n�gra timmar.'),
(2, 7, 'Garnera med gr�dde och jordgubbar.'),

--Kycklingspett
(3, 1, 'R�r ihop kokosgr�dde, socker, salt och curry. Sk�r kycklingen i sm� bitar och blanda ner. L�t marinera i kylen ca 20 minuter.'),
(3, 2, 'Tr� kycklingbitarna p� spett. Stek spetten i 3�4 msk olja i en het stekpanna ca 5 minuter. Pensla med resten av oljan under stekningen s� kycklingen h�ller sig saftig. Toppa spetten med koriander'),


--Ryggbiff
(4, 1, 'S�tt ugnen p� 175�C.'),
(4, 2, 'Rotselleripur�: Skala och sk�r rotsellerin i mindre bitar och l�gg i en ugnsform. L�gg vitl�ksklyftorna (med skal) till vitl�ksskyn i en ugnsform. Rosta rotsellerin och vitl�ken i ugnen ca 30 minuter tills rotsellerin �r mjuk.'),
(4, 3, 'Mixa rotsellerin i en blender med vin�ger och olivolja. Sp�d med vatten till lagom konsistens. Smaka av med salt.'),
(4, 4, 'Vitl�kssky: Skala och skiva l�ken. Skala de rostade vitl�ksklyftorna. Fr�s schalottenl�ken i 2 msk av sm�ret (f�r 8 port) i en kastrull ca 5 minuter. Tills�tt vin�ger och den rostade vitl�ken. L�t koka upp och tills�tt fond och vatten, l�t koka 2�3 minuter. Tills�tt resten av sm�ret lite i taget och mixa till en sl�t s�s vid servering. Sila genom en finmaskig sil och smaka av med salt och peppar.'),
(4, 5, 'S�tt ugnen p� 100�C.'),
(4, 6, 'Ryggbiff: Krydda k�ttet med salt och peppar. Stek i omg�ngar i het panna i olja och sm�r, 1�2 minuter per sida. L�gg k�ttet p� ett ugnsgaller med pl�t under. S�tt in i ugnen, se Innertemperatur f�r blodigt, medium och v�lstekt. L�t k�ttet vila ca 2 minuter innan servering.'),
(4, 7, 'Skiva k�ttet och servera med rotselleripur�n, vitl�ksskyn och toppa med svartk�len.')
