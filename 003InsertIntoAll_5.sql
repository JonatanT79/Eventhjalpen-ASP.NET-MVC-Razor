use TranbarDB 

use TranbarDB 



insert into UserAdress(Adress,ZipCode,City) 

values

('Hornsgatan 92', '118 21','Stockholm'),

('Vallgatan 21', '411 16','Göteborg'),

('Ymersgata 3', '215 36','Malmö'),

('Lekgatan 7', '724 62','Västerås')



Insert into Users(Firstname, Lastname, Email, Phonenumber, UserAdressID)

Values 

('Sven', 'Svensson', 'Sven.Svensson@gmail.com', '070876568',3),

('Anders', 'Borg', 'Anders.Borg@gmail.com','0707604923',1),

('Göran', 'Andersson', 'golle@gmail.com', '071212345',2),

('Eva','Lindgren', 'Eva.Lindgren@gmail.com', '070976557',4),

('Kalle', 'Berg', 'kalle121@gmail.com', '0702350455',1)



Insert into Products(ProductName, Description,Quantity,Price)

Values 

('Smör', 'Svenskt','500 g',49.90), --1

('Ägg','','6 Pack', 17.90), --2

('Strösocker', '','1 Kg', 15.90), --3

('Kakao','','200 g', 30.90), --4

('Florsocker','','500 g', 17.90), --5

('Vispgrädde','40%','3 dl', 18.90), --6

('Vetemjöl','','2 kg', 14.90), --7

('Florsocker','','500 g', 17.90), --8

('Potatismjöl','','500 g', 12.90), --9

('Bakpulver','','225 g', 14.90), --10



('Vaniljsocker','','170 g', 17.90), --11

('Jordgubbssaft','Drickfärdig','50 cl', 41.90), --12

('Jordgubbssylt','Ekologisk','400 g', 28.90), --13

('Jordgubbar','','400 g', 39.90), --14

('Kokosgrädde','','400 ml', 25.90), --15

('Salt','','150 g', 8.90), --16

('Svartpeppar','Grovmalen','41 g', 24.90), --17

('Curry','','34 g', 27.90),  --18

('Kycklingfilé','Frysta','1 g', 90.90),  --19

('Matolja','','500 ml', 25.90), --20



('Rotselleri','','450 g', 9.90),  --21

('Vitvinsvinäger','','50 cl', 35.50),  --22

('Olivolja','','750 ml', 61.00), --23

('Vitlök','3 Pack','100 g', 17.90), --24

('Schalottenlök','','250 g', 18.90),  --25

('Ryggbiff','Färsk',' 180 g', 72.90), --26

('Ströbröd', '', '400 g', 15.90), --27

('Majsstärkelse','', '400 g', 18.90), --28

('Grillspett', 'Bambu', '100 st', 12.90), --29

('Koriander', 'Fryst', '40 g', 11.50), --30

('Kalvfond', 'Koncentrerad', '180 ml', 34.90), --31

('Vatten', '', '', 0.00), --32
('Tomater', 'Kvist', '120 g', 38.95 ), --33
('Mozzarella', '', '250 g', 31.95), --34
('Balsamvinäger', '', '500 ml', 43.95), -- 35
('Basilika', '', '1-p', 20.95), --36
('Rödbetor', 'Ekologiska', '1 kg', 21.95), -- 37
('Rosmarin', 'Torkad', '20 g', 20.95) --38



Insert into RecipeType(RecipeTypeName)

Values 

('Bakverk'), --1

('Förrätt'), --2

('Middag'), --3

('Vegetarisk') --4



---Ändrade recipeTypeID

Insert into Recipe(RecipeTypeID, RecipeName, EstimatedTime)

Values 

(1,'Kladdkaka', 40), --1 

(1,'Jordgubbstårta',50), --2

(3,'Kycklingspett', 70), --3

(3,'Ryggbiff med rotselleripuré', 60), --4

(2, 'Insalata Caprese', 15),

(4, 'Rostade rödbetor', 60)




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

('kryddmått'), --10

('st'), --11

('Klyftor'), --12

('Skivor'), --13
('Kruka') --14



Insert into RecipeDetails(RecipeID, ProductID, ProductQuantity, MeasurementUnitID)




Values 

--

--Kladdkaka

(1, 1, 100, 2),

(1, 3, 2.5, 5),

(1, 2, 2, 11),

(1, 7, 1, 5),

(1, 4, 3, 8),

(1, 11, 1, 9),

(1, 8, 0, 11),

(1, 7, 2, 5),



--Jordgubbstårta

(2, 7, 1, 5),

(2, 9, 1, 9),

(2, 2, 4, 11), -- ÄGG

(2, 3, 2, 5), -- STRÖSOCKER

(2, 1, 0.5, 8),

(2, 27, 1.5, 8),

(2, 6, 1.5, 5), --GRÄDDE

(2, 2, 1, 11), -- OBS DETTA ÄR ETT ÄGG TILL!! --

(2, 3, 2, 9), -- OBS DETTA ÄR MER STRÖSOCKER --

(2, 28, 1, 8),

(2, 11, 2, 9),

(2, 6, 1, 5), -- OBS DETTA ÄR MER GRÄDDE, MEN DENNA GÅNG VISPAD! --

(2, 12, 1, 5),

(2, 13, 2, 5),

(2, 6, 4, 5), -- GRÄDDE IGEN--

(2, 14, 400, 2), --JORDUBBAR, OKLAR MÄNGD



--Kycklingspett

(3, 15, 2, 5),

(3, 3, 1, 8),

(3, 16, 1, 9),

(3, 18, 2, 8),

(3, 19, 600, 2),

(3, 20, 0.5, 5),

(3, 29, 1, 11), -- DETTA ÄR PRODUKTEN GRILLSPETT AV TRÄ (INTE MAT ALLTSÅ)

(3, 30, 3, 8), 



--Ryggbiff

(4, 21, 1, 1),

(4, 22, 2, 8 ),

(4, 23, 0.5, 5),

(4, 32, 0, 11), --OKLAR MÄNGD VATTEN

(4, 24, 3, 12), -- VITLÖKSKLYFTOR, HUR RÄKNA?

(4, 25, 1.5, 11),

(4, 1, 37.5, 2),

(4, 22, 0.75, 8),

(4, 31, 1, 8),

(4, 32, 2.5, 5),

(4, 26, 4, 13),

(4, 16, 1, 9),

(4, 20, 1, 8),

(4, 1, 12.5, 2),




-- Insalata Caprese

(5, 33, 4, 11),
(5, 34, 250, 2),
(5, 16, 1, 10),
(5, 17, 1, 10),
(5, 23, 1, 5),
(5, 35, 3, 8),
(5, 36, 1, 14),


-- Rostade rödbetor med rosmarin

(6, 37, 1.5, 1),
(6, 23, 2, 8),
(6, 38, 1, 9),
(6, 16, 1, 9),
(6, 17, 2, 10)



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

('Bröllop'),

('Buffé')



Insert into EventDetails(RecipeID, EventID)

Values 

(1, 2),

(2, 2),

(3, 1),

(4, 3),

(5, 4),

(6, 4)



Insert into RecipeSteps(RecipeID, Stepnumber,Instructions)

Values 



-- Kladdkaka

(1, 1, 'Sätt ugnen på 175 grader'),

(1, 2, 'Smält smöret i en kastrull. Lyft av kastrullen från plattan'),

(1, 3, 'Rör ner socker och ägg, blanda väl. Rör ner övriga ingredienser så att det blir väl blandat'),

(1, 4, 'Häll smeten i en smord och bröad form med löstagbar kant.'),

(1, 5, 'Grädda mitt i ugnen i cirka 15 min. Kakan blir låg med ganska hård yta och lite kladdig i mitten'),

(1, 6, 'Låt kakan kallna, Pudra över florsocker. Servera med grädde eller glass och frukt'),



--Jordgubbstårta

(2, 1, 'Sätt ugnen på 175 grader. Smörj och bröa formen. Blanda vetemjöl, potatismjöl och bakpulver i en skål. Vispa ägg och socker så att blandningen blir ljus och pösig. Sockret ska ha löst sig helt. Sikta ner mjölblandningen i äggsmeten och blanda.'),

(2, 2, 'Häll upp i formen. Grädda mitt i ugnen ca 35 min.'),

(2, 3, 'Vaniljkräm: Blanda grädde, äggula, socker och majsstärkelse i en kastrull. Sjud under omrörning tills krämen tjocknar. Ta från värmen. Blanda i vaniljsockret. Låt kallna.'),

(2, 4, 'Vänd ner vispad grädde när det är dags att lägga ihop tårtan.'),

(2, 5, 'Ihopläggning: Skär tårtbottnen i tre delar. Pensla bottnarna med saft. Skär jordgubbarna i små bitar och blanda med vaniljkrämen.'),

(2, 6, 'Bred jordgubbssylt och därefter vaniljkräm på de två understa bottnarna. Lägg på den översta bottnen. Plasta in tårtan noga och låt den safta till sig i kylen, gärna några timmar.'),

(2, 7, 'Garnera med grädde och jordgubbar.'),



--Kycklingspett

(3, 1, 'Rör ihop kokosgrädde, socker, salt och curry. Skär kycklingen i små bitar och blanda ner. Låt marinera i kylen ca 20 minuter.'),

(3, 2, 'Trä kycklingbitarna på spett. Stek spetten i 3–4 msk olja i en het stekpanna ca 5 minuter. Pensla med resten av oljan under stekningen så kycklingen håller sig saftig. Toppa spetten med koriander'),



--Ryggbiff

(4, 1, 'Sätt ugnen på 175°C.'),

(4, 2, 'Rotselleripuré: Skala och skär rotsellerin i mindre bitar och lägg i en ugnsform. Lägg vitlöksklyftorna (med skal) till vitlöksskyn i en ugnsform. Rosta rotsellerin och vitlöken i ugnen ca 30 minuter tills rotsellerin är mjuk.'),

(4, 3, 'Mixa rotsellerin i en blender med vinäger och olivolja. Späd med vatten till lagom konsistens. Smaka av med salt.'),

(4, 4, 'Vitlökssky: Skala och skiva löken. Skala de rostade vitlöksklyftorna. Fräs schalottenlöken i 2 msk av smöret (för 8 port) i en kastrull ca 5 minuter. Tillsätt vinäger och den rostade vitlöken. Låt koka upp och tillsätt fond och vatten, låt koka 2–3 minuter. Tillsätt resten av smöret lite i taget och mixa till en slät sås vid servering. Sila genom en finmaskig sil och smaka av med salt och peppar.'),

(4, 5, 'Sätt ugnen på 100°C.'),

(4, 6, 'Ryggbiff: Krydda köttet med salt och peppar. Stek i omgångar i het panna i olja och smör, 1–2 minuter per sida. Lägg köttet på ett ugnsgaller med plåt under. Sätt in i ugnen, se Innertemperatur för blodigt, medium och välstekt. Låt köttet vila ca 2 minuter innan servering.'),
(4, 7, 'Skiva köttet och servera med rotselleripurén, vitlöksskyn och toppa med svartkålen.'),


-- Insalata Caprese

(5, 1, 'Skölj tomaterna. Skär tomaterna och mozzarellan i tunna skivor.'),
(5, 2, 'Varva tomater och mozzarellaskivor på ett fat eller 4 tallrikar (för 4 port).'),
(5, 3, 'Salta och peppra. Droppa olivolja och eventuellt lite balsamvinäger över.'),
(5, 4, 'Strö över plockad basilika. Servera genast.'),


-- Rostade rödbetor med rosmarin

(6, 1, 'Sätt ugnen på 200°C.'),
(6, 2, 'Skala rödbetorna och skär i klyftor. Blanda med olja och kryddor på en plåt. Ställ in i mitten av ugnen ca 40 minuter tills de är mjuka.')
