## AzureBlobStorageLibrary

Przykład biblioteki, do komunikacji z magazynem danych na Azure. Posiada dwie klasy:
* GetFileFromBlobStorage - do pobierania pliku z Azure
* SendFileToBlobStorage - do wysyłania pliku na Azure
DLL do wywołania z pozomiu aplikacji produkcyjno-logistycznych pakietu Rekord.ERP.
Składa sie z dwóch projektów:
* AzureBlobStorageLibrary - właściwy projekt, który tworzy nasz przykładowy DLL
* LaunchAzureBlobStorage - prosty programik do uruchamiania tej DLL-ki pod testy

Dzięki zastosowania własnej technologii do kastomizacji aplikacji opartej na XML, można m.in. podłączać pliki DLL napisane w środowisku Visual Studio. Wymagane środowisko do kompilacji to ASP.NET 4 a klasy dostępne z tego pakietu muszą być oznaczone [ComVisible(true)] i posiadać metodę Execute z 3 parametrami, tak jak w tym przykładzie. Dwa parametry są wejściowe, tekstowe. 

Pierwszy to stała lista parametrów takich jak: ALIAS, USERNAME, PASSWORD, HANDLE, DBPATH, SHAREDCLIHANDLE, NAZWAFIRMY, MIASTOFIRMY i APLIKACJA.
Drugi to lista definiowana w XML. Dla tej DLL-ki takimi parametrami to:
* connectionString - pełny ciąg do połączenia z magazynem na Azure, skopiowany z opcji klucze dostępu w ramach konta magazynu
* container - nazwa kontenera na Azure 
* localFileName - nazwa pliku ze ścieżką na komputerze
* deleteFile - czy usuwać plik
Trzeci parametr to tekst zwracany do Delphi. Dla tej DLL-ki moga to być dwa parametry
* errorMessage - komunikat błędu
* result - komunikat co zostało wykonane
Funkcja zwraca kod o typie integer. Gdy 0 to wszystko OK, gdy wartość -1 to błąd [forum firmy Rekord](https://forum.rekord.com.pl/)

## Licencja
Ten przykład można nieodpłatnie używać, dystrybuować oraz modyfikować [licencja MIT]
[Rekord SI](https://www.rekord.com.pl)
