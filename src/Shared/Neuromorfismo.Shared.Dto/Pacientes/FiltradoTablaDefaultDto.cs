namespace Neuromorfismo.Shared.Dto.Pacientes {
    public class FiltradoTablaDefaultDto {
        public int Page { get; set; } // pagina seleccionada 
        public int PageSize { get; set; } // numero de elementos a mostrar
        public string? SortLabel { get; set; } // columna por la que ordenar
        public int SortDirection { get; set; } // direccion de ordenamiento
        public string? SearchString { get; set; } // string de busqueda
    }
}
