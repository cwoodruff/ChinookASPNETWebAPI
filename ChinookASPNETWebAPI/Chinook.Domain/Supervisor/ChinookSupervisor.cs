using Chinook.Domain.Repositories;
using Chinook.Domain.Validation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor : IChinookSupervisor
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IInvoiceLineRepository _invoiceLineRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IMemoryCache _cache;
        
        private readonly AlbumValidator _albumValidator;
        private readonly ArtistValidator _artistValidator;
        private readonly CustomerValidator _customerValidator;
        private readonly EmployeeValidator _employeeValidator;
        private readonly GenreValidator _genreValidator;
        private readonly InvoiceValidator _invoiceValidator;
        private readonly InvoiceLineValidator _invoiceLineValidator;
        private readonly MediaTypeValidator _mediaTypeValidator;
        private readonly PlaylistValidator _playlistValidator;
        private readonly PlaylistTrackValidator _playlistTrackValidator;
        private readonly TrackValidator _trackValidator;

        public ChinookSupervisor()
        {
        }

        public ChinookSupervisor(IAlbumRepository albumRepository,
            IArtistRepository artistRepository,
            ICustomerRepository customerRepository,
            IEmployeeRepository employeeRepository,
            IGenreRepository genreRepository,
            IInvoiceLineRepository invoiceLineRepository,
            IInvoiceRepository invoiceRepository,
            IMediaTypeRepository mediaTypeRepository,
            IPlaylistRepository playlistRepository,
            ITrackRepository trackRepository,
            IMemoryCache memoryCache
        )
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _genreRepository = genreRepository;
            _invoiceLineRepository = invoiceLineRepository;
            _invoiceRepository = invoiceRepository;
            _mediaTypeRepository = mediaTypeRepository;
            _playlistRepository = playlistRepository;
            _trackRepository = trackRepository;
            _cache = memoryCache;
            
            _albumValidator = new AlbumValidator();
            _artistValidator = new ArtistValidator();
            _customerValidator = new CustomerValidator();
            _employeeValidator = new EmployeeValidator();
            _genreValidator = new GenreValidator();
            _invoiceValidator = new InvoiceValidator();
            _invoiceLineValidator = new InvoiceLineValidator();
            _mediaTypeValidator = new MediaTypeValidator();
            _playlistValidator = new PlaylistValidator();
            _playlistTrackValidator = new PlaylistTrackValidator();
            _trackValidator = new TrackValidator();
        }
    }
}