﻿using System.Collections.ObjectModel;
using static MedSestriManipulations.MainPage;

namespace MedSestriManipulations
{
    public static class MedicalProcedureService
    {
        public static ObservableCollection<MedicalProcedureViewModel> GetAllProcedures()
        {
            return new ObservableCollection<MedicalProcedureViewModel>
            {
                new MedicalProcedureViewModel { Name = "Пълна кръвна картина + ДКК + СУЕ", Price = 12.0m },
                new MedicalProcedureViewModel { Name = "Протромбиново време", Price = 6.0m },
                new MedicalProcedureViewModel { Name = "Фибриноген", Price = 6.0m },
                new MedicalProcedureViewModel { Name = "Д-димер(D-dimer)", Price = 25.0m },
                new MedicalProcedureViewModel { Name = "TSH", Price = 17.0m },
                new MedicalProcedureViewModel { Name = "Глюкоза", Price = 3.0m },
                new MedicalProcedureViewModel { Name = "Общ белтък", Price = 3.5m },
                new MedicalProcedureViewModel { Name = "Липиден профил", Price = 12.0m },
                new MedicalProcedureViewModel { Name = "Албумин", Price = 3.5m },
                new MedicalProcedureViewModel { Name = "Билирубин, общ", Price = 3.5m },
                new MedicalProcedureViewModel { Name = "Билирубин, директен (конюгиран)", Price = 3.5m },
                new MedicalProcedureViewModel { Name = "Желязо (Fe)", Price = 6.0m },
                new MedicalProcedureViewModel { Name = "АсАТ", Price = 3.5m },
                new MedicalProcedureViewModel { Name = "АлАТ", Price = 3.5m },
                new MedicalProcedureViewModel { Name = "ГГТ", Price = 3.5m },
                new MedicalProcedureViewModel { Name = "ЖСК", Price = 6.0m },
                //new MedicalProcedureViewModel { Name = "Креатинин", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Урея", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Пикочна киселина", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Натрий (Na+)", Price = 4.5m },
                //new MedicalProcedureViewModel { Name = "Калий (K+)", Price = 4.5m },
                //new MedicalProcedureViewModel { Name = "Общ калций (Ca)", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Магнезий (Mg)", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Витамин D (25-OH)", Price = 30.0m },
                //new MedicalProcedureViewModel { Name = "Фолиева киселина", Price = 24.0m },
                //new MedicalProcedureViewModel { Name = "Витамин B12", Price = 23.0m },
                //new MedicalProcedureViewModel { Name = "HbA1C – гликиран хемоглобин", Price = 15.0m },
                //new MedicalProcedureViewModel { Name = "CRP – количествено измерване", Price = 7.0m },
                //new MedicalProcedureViewModel { Name = "Урина 10 пок. + седимент", Price = 4.5m },
                //new MedicalProcedureViewModel { Name = "Здравна книжка", Price = 25.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Ендобиогенна медицина (Жени/Деца под 12г.", Price = 181.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Ендобиогенна медицина (Мъже)", Price = 181.0m },
                //new MedicalProcedureViewModel { Name = "Щитовидна жлеза", Price = 43.0m },
                //new MedicalProcedureViewModel { Name = "Хашимото", Price = 45.0m },
                //new MedicalProcedureViewModel { Name = "Щитовидна жлеза – разширен", Price = 69.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Витамини В1, В2, В6", Price = 100.0m },
                //new MedicalProcedureViewModel { Name = "Полови хормони", Price = 56.0m },
                //new MedicalProcedureViewModel { Name = "Предоперативен – минимален", Price = 30.0m },
                //new MedicalProcedureViewModel { Name = "Предоперативен – оптимален", Price = 80.0m },
                //new MedicalProcedureViewModel { Name = "Начало на бременност", Price = 72.0m },
                //new MedicalProcedureViewModel { Name = "Начало на бременност – TORCH панел", Price = 75.0m },
                //new MedicalProcedureViewModel { Name = "Начало на бременност – TORCH панел IgM", Price = 90.0m },
                //new MedicalProcedureViewModel { Name = "Начало на бременност – TORCH панел разширен", Price = 160.0m },
                //new MedicalProcedureViewModel { Name = "HPV + полово предавани инфекции (4 патогена)", Price = 130.0m },
                //new MedicalProcedureViewModel { Name = "HPV + полово предавани инфекции (7 патогена)", Price = 150.0m },
                //new MedicalProcedureViewModel { Name = "HPV + полово предавани инфекции (12 патогена)", Price = 170.0m },
                //new MedicalProcedureViewModel { Name = "Анемия", Price = 60.0m },
                //new MedicalProcedureViewModel { Name = "Болест на Алцхаймер и деменции", Price = 350.0m },
                //new MedicalProcedureViewModel { Name = "Кардио", Price = 50.0m },
                //new MedicalProcedureViewModel { Name = "Сърдечно-съдов риск", Price = 88.0m },
                //new MedicalProcedureViewModel { Name = "Пакет остеопороза", Price = 82.0m },
                //new MedicalProcedureViewModel { Name = "Надбъбрек", Price = 80.0m },
                //new MedicalProcedureViewModel { Name = "Най-важните витамини", Price = 60.0m },
                //new MedicalProcedureViewModel { Name = "Детска градина – фецес", Price = 23.0m },
                //new MedicalProcedureViewModel { Name = "Детска градина – пълен", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "Чревен Микробиом", Price = 315.0m },
                //new MedicalProcedureViewModel { Name = "Veggie-Check Basis", Price = 21.0m },
                //new MedicalProcedureViewModel { Name = "Veggie-Check Standard", Price = 101.0m },
                //new MedicalProcedureViewModel { Name = "Veggie-Check Plus", Price = 241.0m },
                //new MedicalProcedureViewModel { Name = "Хронична умора", Price = 180.0m },
                //new MedicalProcedureViewModel { Name = "Свободен Т4 (FT4)", Price = 17.0m },
                //new MedicalProcedureViewModel { Name = "Свободен Т3 (FT3)", Price = 17.0m },
                //new MedicalProcedureViewModel { Name = "Анти TG антитела (ТАТ)", Price = 18.0m },
                //new MedicalProcedureViewModel { Name = "Анти TPO антитела (MAT)", Price = 18.0m },
                //new MedicalProcedureViewModel { Name = "TSH рецепторни антитела (TSH-R-Ab)", Price = 37.0m },
                //new MedicalProcedureViewModel { Name = "Лутеинизиращ хормон (LH)", Price = 17.0m },
                //new MedicalProcedureViewModel { Name = "Фоликулостимулиращ хормон (FSH)", Price = 17.0m },
                //new MedicalProcedureViewModel { Name = "Пролактин", Price = 17.0m },
                //new MedicalProcedureViewModel { Name = "Естрадиол", Price = 19.0m },
                //new MedicalProcedureViewModel { Name = "Анти-Мюлеров хормон (AMH)", Price = 53.0m },
                //new MedicalProcedureViewModel { Name = "Тестостерон (Testosterone)", Price = 19.0m },
                //new MedicalProcedureViewModel { Name = "17 – хидрокси-прогестерон (17-OH-Progesterone)", Price = 27.0m },
                //new MedicalProcedureViewModel { Name = "Андростендион", Price = 27.0m },
                //new MedicalProcedureViewModel { Name = "Дехидроепиандростендион сулфат (DHEA-s)", Price = 20.0m },
                //new MedicalProcedureViewModel { Name = "SHBG", Price = 24.0m },
                //new MedicalProcedureViewModel { Name = "Растежен хормон (STH)", Price = 24.0m },
                //new MedicalProcedureViewModel { Name = "Адренокортикотропен хормон (ACTH)", Price = 24.0m },
                //new MedicalProcedureViewModel { Name = "Кортизол (серум) (Cortisol serum)", Price = 18.0m },
                //new MedicalProcedureViewModel { Name = "Кортизол (слюнка) (Cortisol saliva)", Price = 18.0m },
                //new MedicalProcedureViewModel { Name = "Инсулин (IRI)", Price = 18.0m },
                //new MedicalProcedureViewModel { Name = "C-пептид (C – peptide)", Price = 22.0m },
                //new MedicalProcedureViewModel { Name = "Паратхормон (PTH)", Price = 30.0m },
                //new MedicalProcedureViewModel { Name = "Остеокалцин", Price = 22.0m },
                //new MedicalProcedureViewModel { Name = "β-CrossLaps", Price = 22.0m },
                //new MedicalProcedureViewModel { Name = "Инсулиноподобен фактор на растежа - IGF 1", Price = 44.0m },
                //new MedicalProcedureViewModel { Name = "Алдостерон", Price = 40.0m },
                //new MedicalProcedureViewModel { Name = "Ренин", Price = 45.0m },
                //new MedicalProcedureViewModel { Name = "Адреналин – плазма", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "Адреналин – урина", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "Норадреналин – плазма", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "Норадреналин – урина", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "Допамин – плазма", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "Допамин – урина", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "3-метокситирамин (free) – плазма", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "3-метокситирамин – урина (free) –", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Катехоламини (Адреналин, норадреналин и допамин) – урина", Price = 135.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Метанефрин, норметанефрин и 3-метокситирамин – урина", Price = 130.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Катехоламини (Адреналин, норадреналин, допамин) и метанефрин, норметанефрин и 3-метокситирамин – урина", Price = 200.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Адреналин и Норадреналин – урина", Price = 92.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Метанефрин (free) и Норметанефрин (free) – урина", Price = 92.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Метанефрин (free) и Норметанефрин (free) – плазма", Price = 92.0m },
                //new MedicalProcedureViewModel { Name = "Серотонин", Price = 58.0m },
                //new MedicalProcedureViewModel { Name = "CA 15-3", Price = 21.0m },
                //new MedicalProcedureViewModel { Name = "CA 125", Price = 21.0m },
                //new MedicalProcedureViewModel { Name = "HE 4", Price = 57.0m },
                //new MedicalProcedureViewModel { Name = "HE 4 + CA 125 + ROMA index", Price = 65.0m },
                //new MedicalProcedureViewModel { Name = "CA 19-9", Price = 21.0m },
                //new MedicalProcedureViewModel { Name = "CEA (Карциноембрионален антиген)", Price = 21.0m },
                //new MedicalProcedureViewModel { Name = "Алфа-фетопротеин (AFP)", Price = 21.0m },
                //new MedicalProcedureViewModel { Name = "CA 72-4", Price = 23.0m },
                //new MedicalProcedureViewModel { Name = "Човешки хорионгонадотропин (hHG + ß)", Price = 20.0m },
                //new MedicalProcedureViewModel { Name = "Простат-специфичен антиген, общ (tPSA)", Price = 20.0m },
                //new MedicalProcedureViewModel { Name = "Свободен простат-специфичен антиген (fPSA)", Price = 20.0m },
                //new MedicalProcedureViewModel { Name = "tPSA + fPSA", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "Тиреоглобулин", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "Калцитонин", Price = 33.0m },
                //new MedicalProcedureViewModel { Name = "Неврон-специфична енолаза (NSE)", Price = 32.0m },
                //new MedicalProcedureViewModel { Name = "S100", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Cyfra 21-1", Price = 22.0m },
                //new MedicalProcedureViewModel { Name = "Пълна кръвна картина (ПКК)", Price = 7.0m },
                //new MedicalProcedureViewModel { Name = "Пълна кръвна картина + СУЕ", Price = 8.0m },
                //new MedicalProcedureViewModel { Name = "ДКК (апаратно)", Price = 3.0m },
                //new MedicalProcedureViewModel { Name = "ДКК (микроскопско)", Price = 5.0m },
                //new MedicalProcedureViewModel { Name = "СУЕ", Price = 3.0m },
                //new MedicalProcedureViewModel { Name = "Морфология на Еритроцити", Price = 6.0m },
                //new MedicalProcedureViewModel { Name = "Ретикулоцити", Price = 4.0m },
                //new MedicalProcedureViewModel { Name = "APTT", Price = 6.0m },
                //new MedicalProcedureViewModel { Name = "Протеин С", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Протеин S", Price = 41.0m },
                //new MedicalProcedureViewModel { Name = "Лупусни антикоагуланти", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Антитромбин III", Price = 25.0m },
                //new MedicalProcedureViewModel { Name = "Кръвно-захарен профил / ОГТТ (трикратно)", Price = 9.0m },
                //new MedicalProcedureViewModel { Name = "Аполипопротеин А1 (Аро А1)", Price = 15.0m },
                //new MedicalProcedureViewModel { Name = "Аполипопротеин В1 (Аро В)", Price = 15.0m },
                //new MedicalProcedureViewModel { Name = "Липопротеин (а) (Lp (a))", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "Цистатин С", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "Общ холестерол (Chol.)", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "HDL – Холестерол (HDL-C)", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "LDL – Холестерол (LDL-C)", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Триглицериди (Tg)", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Алкална фосфатаза", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Амилаза", Price = 7.0m },
                //new MedicalProcedureViewModel { Name = "Липаза", Price = 7.0m },
                //new MedicalProcedureViewModel { Name = "Лактатдехидрогеназа (LDH)", Price = 6.5m },
                //new MedicalProcedureViewModel { Name = "Креатин фосфокиназа (CPK)", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Фракцията на креатинкиназата (CK-MB)", Price = 10.0m },
                //new MedicalProcedureViewModel { Name = "Йонизиран калций (Са++)", Price = 4.5m },
                //new MedicalProcedureViewModel { Name = "Неорганични фосфати (PO₄)", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Хлор (Cl-)", Price = 4.5m },
                //new MedicalProcedureViewModel { Name = "Витамин B1", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "Витамин B2", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "Витамин B6", Price = 48.0m },
                //new MedicalProcedureViewModel { Name = "Хомоцистеин", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "Феритин", Price = 18.0m },
                //new MedicalProcedureViewModel { Name = "Трансферин", Price = 18.0m },
                //new MedicalProcedureViewModel { Name = "hs – CRP", Price = 15.0m },
                //new MedicalProcedureViewModel { Name = "Тропонин", Price = 28.0m },
                //new MedicalProcedureViewModel { Name = "NT-Pro-BNP", Price = 60.0m },
                //new MedicalProcedureViewModel { Name = "Прокалцитонин (РСТ)", Price = 50.0m },
                //new MedicalProcedureViewModel { Name = "Имуноглобулини IgA", Price = 16.0m },
                //new MedicalProcedureViewModel { Name = "Имуноглобулини IgG", Price = 16.0m },
                //new MedicalProcedureViewModel { Name = "Имуноглобулини IgM", Price = 16.0m },
                //new MedicalProcedureViewModel { Name = "Имуноглобулини IgE, общи", Price = 20.0m },
                //new MedicalProcedureViewModel { Name = "Калпротектин", Price = 42.0m },
                //new MedicalProcedureViewModel { Name = "CERTEST", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "FOB + Калпротектин + Трансферин + Лактоферин", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "Панкреатична еластаза", Price = 55.0m },
                //new MedicalProcedureViewModel { Name = "Автоеритроантителата (диференциран директен Coombs)", Price = 30.0m },
                //new MedicalProcedureViewModel { Name = "Кръвна група – АВО, Rh фактор", Price = 20.0m },
                //new MedicalProcedureViewModel { Name = "Определяне на подгрупите на А антигена (А1 и А2) с тест-реагенти с анти-А и анти-Н", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "Определяне на слаб D антиген (Du) по индиректен тест на Coombs", Price = 50.0m },
                //new MedicalProcedureViewModel { Name = "Изследване за автоеритроантитела при фиксирани антитела върху еритроцитите – директен Coombs", Price = 40.0m },
                //new MedicalProcedureViewModel { Name = "Изследване за алоеритроантитела", Price = 45.0m },
                //new MedicalProcedureViewModel { Name = "Определяне на Rh фенотип (СсDЕе) и Kell антиген", Price = 45.0m },
                //new MedicalProcedureViewModel { Name = "Комбиниран тест за алергии ALEX 2- Allergy Explorer (sIgE)", Price = 360.0m },
                //new MedicalProcedureViewModel { Name = "Food Xplorer (FOX) хранителен скрининг (283 храни – непоносимост IgG)", Price = 360.0m },
                //new MedicalProcedureViewModel { Name = "Пълен панел Микробиом", Price = 967.0m },
                //new MedicalProcedureViewModel { Name = "Разширен панел Микробиом", Price = 380.0m },
                //new MedicalProcedureViewModel { Name = "Стандартен панел Микробиом", Price = 315.0m },
                //new MedicalProcedureViewModel { Name = "Панкреатична еластаза", Price = 55.0m },
                //new MedicalProcedureViewModel { Name = "Секреторен имуноглобулин A (sIgA)", Price = 38.0m },
                //new MedicalProcedureViewModel { Name = "Дефензин", Price = 47.0m },
                //new MedicalProcedureViewModel { Name = "Зонулин", Price = 85.0m },
                //new MedicalProcedureViewModel { Name = "Алфа-1-антитрипсин", Price = 37.0m },
                //new MedicalProcedureViewModel { Name = "Калпротектин (количествен тест)", Price = 42.0m },
                //new MedicalProcedureViewModel { Name = "Лактоферин (количествен тест)", Price = 73.0m },
                //new MedicalProcedureViewModel { Name = "Окултна кръв", Price = 10.0m },
                //new MedicalProcedureViewModel { Name = "Пакетно изследване на Калпротектин, Лактоферин, Хемоглобин, Трансферин (качествен тест)", Price = 35.0m },
                //new MedicalProcedureViewModel { Name = "Хистамин", Price = 66.0m },
                //new MedicalProcedureViewModel { Name = "Гастроинтестинален панел (Real-time PCR)", Price = 120.0m },
                //new MedicalProcedureViewModel { Name = "Пакетно изследване за Shigella + Salmonella + Escherichia coli + Yersinia + Campylobacter + Clostridium difficile toxin A+B + Candida + Helicobacter pylori", Price = 92.0m },
                //new MedicalProcedureViewModel { Name = "Пакетно изследване за Shigella + Salmonella + Escherichia coli + Yersinia + Campylobacter + Clostridium difficile toxin A+B + Candida + Helicobacter pylori + Окултна кръв", Price = 100.0m },
                //new MedicalProcedureViewModel { Name = "Хеликобактер пилори антиген тест (фецес)", Price = 40.0m },
                //new MedicalProcedureViewModel { Name = "Хеликобактер пилори IgG Ab (кръв)", Price = 22.0m },
                //new MedicalProcedureViewModel { Name = "Ротавирус + Аденовирус (качествен тест)", Price = 22.0m },
                //new MedicalProcedureViewModel { Name = "Морфологично изследване за чревни паразити + Cryptosporidium + Entamoeba + Giardia Lamblia", Price = 55.0m },
                //new MedicalProcedureViewModel { Name = "Коклюш (Bordetella)", Price = 85.0m },
                //new MedicalProcedureViewModel { Name = "Лактозна непоносимост (Lactose intolerance)", Price = 100.0m },
                //new MedicalProcedureViewModel { Name = "Малария (Malaria)", Price = 110.0m },
                //new MedicalProcedureViewModel { Name = "HPV – DNA 28 Detection", Price = 125.0m },
                //new MedicalProcedureViewModel { Name = "PCR Полово предавани инфекции – 4 патогена", Price = 70.0m },
                //new MedicalProcedureViewModel { Name = "PCR Полово предавани инфекции – 7 патогена", Price = 100.0m },
                //new MedicalProcedureViewModel { Name = "PCR Полово предавани инфекции – 12 патогена", Price = 130.0m },
                //new MedicalProcedureViewModel { Name = "HPV + полово предавани инфекции (4 патогена)", Price = 130.0m },
                //new MedicalProcedureViewModel { Name = "HPV + полово предавани инфекции (7 патогена)", Price = 150.0m },
                //new MedicalProcedureViewModel { Name = "HPV + полово предавани инфекции (12 патогена)", Price = 170.0m },
                //new MedicalProcedureViewModel { Name = "HPV – DNA", Price = 90.0m },
                //new MedicalProcedureViewModel { Name = "Устройство за самостоятелно пробовземане за изследване на HPV – DNA", Price = 10.0m },
                //new MedicalProcedureViewModel { Name = "Хепатит Б (HBV-DNA)", Price = 180.0m },
                //new MedicalProcedureViewModel { Name = "Хепатит C (HCV-RNA)", Price = 150.0m },
                //new MedicalProcedureViewModel { Name = "PCR Гастроинтестинален панел – 14 патогена", Price = 120.0m },
                //new MedicalProcedureViewModel { Name = "Епщайн Бар вирус (EBV-DNA)", Price = 198.0m },
                //new MedicalProcedureViewModel { Name = "Цитомегаловирус (CMV-DNA)", Price = 150.0m },
                //new MedicalProcedureViewModel { Name = "Коронавирус SARS-CoV-2 (COVID-19)", Price = 80.0m },
                //new MedicalProcedureViewModel { Name = "Панел Респираторни вируси", Price = 160.0m },
                //new MedicalProcedureViewModel { Name = "Вродени тромбофилии", Price = 150.0m },
                //new MedicalProcedureViewModel { Name = "PAI", Price = 70.0m },
                //new MedicalProcedureViewModel { Name = "Панел Вродени тромбофилии + PAI", Price = 180.0m },
                //new MedicalProcedureViewModel { Name = "Панел вродени тромбофилии – разширен", Price = 210.0m },
                //new MedicalProcedureViewModel { Name = "PCR – HLA-B27", Price = 77.0m },
                //new MedicalProcedureViewModel { Name = "Коклюш (Bordetella)", Price = 85.0m },
                //new MedicalProcedureViewModel { Name = "Лактозна непоносимост (Lactose intolerance)", Price = 100.0m },
                //new MedicalProcedureViewModel { Name = "Малария (Malaria)", Price = 110.0m },
                //new MedicalProcedureViewModel { Name = "HPV – DNA 28 Detection", Price = 125.0m },
                //new MedicalProcedureViewModel { Name = "PCR Полово предавани инфекции – 4 патогена", Price = 70.0m },
                //new MedicalProcedureViewModel { Name = "PCR Полово предавани инфекции – 7 патогена", Price = 100.0m },
                //new MedicalProcedureViewModel { Name = "PCR Полово предавани инфекции – 12 патогена", Price = 130.0m },
                //new MedicalProcedureViewModel { Name = "HPV + полово предавани инфекции (4 патогена)", Price = 130.0m },
                //new MedicalProcedureViewModel { Name = "HPV + полово предавани инфекции (7 патогена)", Price = 150.0m },
                //new MedicalProcedureViewModel { Name = "HPV + полово предавани инфекции (12 патогена)", Price = 170.0m },
                //new MedicalProcedureViewModel { Name = "HPV – DNA", Price = 90.0m },
                //new MedicalProcedureViewModel { Name = "Устройство за самостоятелно пробовземане за изследване на HPV – DNA", Price = 10.0m },
                //new MedicalProcedureViewModel { Name = "Хепатит Б (HBV-DNA)", Price = 180.0m },
                //new MedicalProcedureViewModel { Name = "Хепатит C (HCV-RNA)", Price = 150.0m },
                //new MedicalProcedureViewModel { Name = "PCR Гастроинтестинален панел – 14 патогена", Price = 120.0m },
                //new MedicalProcedureViewModel { Name = "Епщайн Бар вирус (EBV-DNA)", Price = 198.0m },
                //new MedicalProcedureViewModel { Name = "Цитомегаловирус (CMV-DNA)", Price = 150.0m },
                //new MedicalProcedureViewModel { Name = "Коронавирус SARS-CoV-2 (COVID-19)", Price = 80.0m },
                //new MedicalProcedureViewModel { Name = "Панел Респираторни вируси", Price = 160.0m },
                //new MedicalProcedureViewModel { Name = "Вродени тромбофилии", Price = 150.0m },
                //new MedicalProcedureViewModel { Name = "PAI", Price = 70.0m },
                //new MedicalProcedureViewModel { Name = "Панел Вродени тромбофилии + PAI", Price = 180.0m },
                //new MedicalProcedureViewModel { Name = "Панел вродени тромбофилии – разширен", Price = 210.0m },
                //new MedicalProcedureViewModel { Name = "PCR – HLA-B27", Price = 77.0m },
                //new MedicalProcedureViewModel { Name = "Общо химично изследване", Price = 3.0m },
                //new MedicalProcedureViewModel { Name = "Седимент", Price = 2.0m },
                //new MedicalProcedureViewModel { Name = "Белтък в 24ч. урина", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Микроалбуминурия", Price = 15.0m },
                //new MedicalProcedureViewModel { Name = "Креатинин", Price = 3.0m },
                //new MedicalProcedureViewModel { Name = "Албумин/Креатинин", Price = 18.0m },
                //new MedicalProcedureViewModel { Name = "Общ белтък/креатинин", Price = 7.0m },
                //new MedicalProcedureViewModel { Name = "Пикочна киселина/креатинин", Price = 7.0m },
                //new MedicalProcedureViewModel { Name = "Клирънс на креатинин", Price = 7.0m },
                //new MedicalProcedureViewModel { Name = "Клирънс на пикочна киселина", Price = 7.0m },
                //new MedicalProcedureViewModel { Name = "Урея в 24ч. урина", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Калций в 24ч. урина", Price = 4.0m },
                //new MedicalProcedureViewModel { Name = "Фосфати в 24ч. урина", Price = 4.0m },
                //new MedicalProcedureViewModel { Name = "Натрий в 24ч. урина", Price = 5.0m },
                //new MedicalProcedureViewModel { Name = "Калий в 24ч. урина", Price = 5.0m },
                //new MedicalProcedureViewModel { Name = "алфа-амилаза в урина", Price = 7.0m },
                //new MedicalProcedureViewModel { Name = "C-peptid в 24ч. урина", Price = 18.0m },
                //new MedicalProcedureViewModel { Name = "Кортизол в 24ч. урина", Price = 60.0m },
                //new MedicalProcedureViewModel { Name = "Наркотични вещества", Price = 25.0m },
                //new MedicalProcedureViewModel { Name = "Пикочна киселина в 24ч. урина", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Креатинин в 24ч. урина", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Пикочна киселина в урина", Price = 3.5m },
                //new MedicalProcedureViewModel { Name = "Mg в 24ч. урина", Price = 4.0m },
                //new MedicalProcedureViewModel { Name = "Арсен (As)", Price = 45.0m },
                //new MedicalProcedureViewModel { Name = "Бисмут (Bi)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Живак (Hg)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Йод (I)", Price = 66.0m },
                //new MedicalProcedureViewModel { Name = "Кобалт (Co)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Манган (Mn)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Мед (Cu)", Price = 25.0m },
                //new MedicalProcedureViewModel { Name = "Молибден (Mo)", Price = 60.0m },
                //new MedicalProcedureViewModel { Name = "Никел (Ni)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Паладий (Pd)", Price = 65.0m },
                //new MedicalProcedureViewModel { Name = "Селен (Se)", Price = 30.0m },
                //new MedicalProcedureViewModel { Name = "Талий (Tl)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Хром (Cr)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Цинк (Zn)", Price = 15.0m },
                //new MedicalProcedureViewModel { Name = "Алуминий (Al)", Price = 45.0m },
                //new MedicalProcedureViewModel { Name = "Олово (Pb)", Price = 25.0m },
                //new MedicalProcedureViewModel { Name = "Барий (Ba)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Кадмий (Cd)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Платина (Pt)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Калай (Sn)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Ванадий (V)", Price = 36.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Мед + Цинк", Price = 32.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Мед + Цинк + Селен", Price = 58.0m },
                //new MedicalProcedureViewModel { Name = "Пакет Цинк + Селен", Price = 40.0m },
                //new MedicalProcedureViewModel { Name = "Fibrotest", Price = 220.0m },
                //new MedicalProcedureViewModel { Name = "FibroMax", Price = 300.0m },
                //new MedicalProcedureViewModel { Name = "Veracity (основен панел – Т21, Т18, Т13)", Price = 687.0m },
                //new MedicalProcedureViewModel { Name = "Veracity (основен панел и пол на плода)", Price = 699.0m },
                //new MedicalProcedureViewModel { Name = "Veracity (разширен панел) (Т21, Т18, Т13; анеуплоидии X, Y; пол на плода)", Price = 960.0m },
                //new MedicalProcedureViewModel { Name = "Veracity (разширен панел + микроделеции)", Price = 960.0m },
                //new MedicalProcedureViewModel { Name = "Veragene (Veracity разширен панел + микроделеции + 100 моногенни заболявания)", Price = 1170.0m },
                //new MedicalProcedureViewModel { Name = "Adventia А-Thalassemia", Price = 235.0m },
                //new MedicalProcedureViewModel { Name = "Adventia B-Haemoglobinopathies", Price = 235.0m },
                //new MedicalProcedureViewModel { Name = "Adventia Cystic Fibrosis", Price = 445.0m },
                //new MedicalProcedureViewModel { Name = "Adventia Duchenne Muscular Dystrophy", Price = 285.0m },
                //new MedicalProcedureViewModel { Name = "Adventia Fragile X", Price = 285.0m },
                //new MedicalProcedureViewModel { Name = "Adventia Spinal Muscular Atrophy", Price = 235.0m },
                //new MedicalProcedureViewModel { Name = "Adventia – Основен панел", Price = 785.0m },
                //new MedicalProcedureViewModel { Name = "Adventia – Разширен панел за един пациент", Price = 1045.0m },
                //new MedicalProcedureViewModel { Name = "Adventia – Разширен панел за двама", Price = 1675.0m },
                //new MedicalProcedureViewModel { Name = "Конвенционална цитонамазка за профилактика на РМШ (рак на маточната шийка)", Price = 30.0m },
                //new MedicalProcedureViewModel { Name = "Течно-базирана цитология за РМШ", Price = 50.0m },
                //new MedicalProcedureViewModel { Name = "Хистология – биопсичен материал до 10мм. /2бл./", Price = 55.0m },
                //new MedicalProcedureViewModel { Name = "Хистология – биопсичен материал до 10мм. /5бл./", Price = 95.0m },
                //new MedicalProcedureViewModel { Name = "Спермограма", Price = 160.0m },
            };
        }
    }
}