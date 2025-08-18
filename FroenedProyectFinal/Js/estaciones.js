
const API_BASE_URL = 'https://localhost:7044/api/Estacion'; // 🔧 Cambia por tu URL

// Variables globales
let estaciones = [];
let currentSection = 'view';

// ===========================
// INICIALIZACIÓN
// ===========================

// Inicializar la aplicación cuando el DOM esté cargado
document.addEventListener('DOMContentLoaded', function() {
    console.log('🚀 Iniciando Sistema de Gestión de Estaciones');
    loadEstaciones();
});

// ===========================
// GESTIÓN DE SECCIONES
// ===========================

/**
 * Mostrar una sección específica del menú
 * @param {string} section - Nombre de la sección (view, create, update, delete)
 */
function showSection(section) {
    // Ocultar todas las secciones
    document.querySelectorAll('.content-section').forEach(s => s.classList.remove('active'));
    
    // Mostrar la sección seleccionada
    document.getElementById(`${section}-section`).classList.add('active');
    currentSection = section;

    // Cargar datos si es necesario
    if (section === 'view') {
        loadEstaciones();
    }
    
    console.log(`📱 Sección activa: ${section}`);
}

// ===========================
// SISTEMA DE ALERTAS
// ===========================

/**
 * Mostrar alerta al usuario
 * @param {string} message - Mensaje a mostrar
 * @param {string} type - Tipo de alerta (success, error)
 */
function showAlert(message, type = 'success') {
    const alertContainer = document.getElementById('alertContainer');
    const alert = document.createElement('div');
    alert.className = `alert alert-${type}`;
    alert.textContent = message;
    
    // Limpiar alertas anteriores
    alertContainer.innerHTML = '';
    alertContainer.appendChild(alert);
    
    // Auto-remover después de 5 segundos
    setTimeout(() => {
        if (alert.parentNode) {
            alert.remove();
        }
    }, 5000);
    
    console.log(`🔔 Alerta: ${message} (${type})`);
}

// ===========================
// OPERACIONES CON LA API
// ===========================

/**
 * Cargar todas las estaciones desde la API
 */
async function loadEstaciones() {
    const loading = document.getElementById('loading-view');
    const tableBody = document.getElementById('estacionesTableBody');
    
    loading.style.display = 'flex';
    console.log('📡 Cargando estaciones...');
    
    try {
        const response = await fetch(`${API_BASE_URL}/Estacion`);
        
        if (!response.ok) {
            throw new Error(`HTTP ${response.status}: Error al cargar estaciones`);
        }
        
        estaciones = await response.json();
        console.log(`✅ ${estaciones.length} estaciones cargadas`);
        
        renderEstacionesTable(estaciones);
        
    } catch (error) {
        console.error('❌ Error:', error);
        showAlert('Error al cargar las estaciones: ' + error.message, 'error');
        tableBody.innerHTML = '<tr><td colspan="5" style="text-align: center; padding: 40px; color: #999;">⚠️ Error al cargar los datos</td></tr>';
    } finally {
        loading.style.display = 'none';
    }
}

/**
 * Crear una nueva estación
 * @param {Event} event - Evento del formulario
 */
async function createEstacion(event) {
    event.preventDefault();
    console.log('➕ Creando nueva estación...');
    
    // Recopilar datos del formulario
    const formData = {
        numero: parseInt(document.getElementById('createNumero').value),
        nombre: document.getElementById('createNombre').value.trim(),
        turno: document.getElementById('createTurno').value,
        disponible: document.getElementById('createDisponible').checked
    };

    // Validaciones básicas
    if (!formData.nombre) {
        showAlert('❌ El nombre de la estación es requerido', 'error');
        return;
    }

    if (formData.numero <= 0) {
        showAlert('❌ El número de estación debe ser mayor a 0', 'error');
        return;
    }

    try {
        const response = await fetch(`${API_BASE_URL}/Create`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(errorText || 'Error al crear la estación');
        }

        console.log('✅ Estación creada exitosamente');
        showAlert('✅ Estación creada exitosamente');
        resetForm('createForm');
        loadEstaciones();
        showSection('view');
        
    } catch (error) {
        console.error('❌ Error al crear:', error);
        showAlert('❌ Error al crear la estación: ' + error.message, 'error');
    }
}

/**
 * Actualizar una estación existente
 * @param {Event} event - Evento del formulario
 */
async function updateEstacion(event) {
    event.preventDefault();
    console.log('✏️ Actualizando estación...');
    
    const id = document.getElementById('updateId').value;
    const formData = {
        numero: parseInt(document.getElementById('updateNumero').value),
        nombre: document.getElementById('updateNombre').value.trim(),
        turno: document.getElementById('updateTurno').value,
        disponible: document.getElementById('updateDisponible').checked,
        updateTime: new Date().toISOString()
    };

    // Debug - mostrar datos que se envían
    console.log('📤 Datos a enviar:', {
        id: id,
        url: `${API_BASE_URL}/${id}`,
        formData: formData
    });

    try {
        const response = await fetch(`${API_BASE_URL}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });

        // Debug - mostrar respuesta
        console.log('📥 Respuesta del servidor:', {
            status: response.status,
            statusText: response.statusText,
            ok: response.ok
        });

        if (!response.ok) {
            const errorText = await response.text();
            console.error('❌ Error del servidor:', errorText);
            throw new Error(errorText || `Error HTTP ${response.status}: ${response.statusText}`);
        }

        console.log('✅ Estación actualizada exitosamente');
        showAlert('✅ Estación actualizada exitosamente');
        cancelUpdate();
        loadEstaciones();
        showSection('view');
        
    } catch (error) {
        console.error('❌ Error al actualizar:', error);
        showAlert('❌ Error al actualizar la estación: ' + error.message, 'error');
    }
}

/**
 * Eliminar una estación usando EstacionId correcto
 * @param {number} numero - Número de la estación a eliminar
 */
async function deleteEstacion(numero) {
    console.log(`🗑️ Eliminando estación ${numero}...`);
    
    try {
        // Buscar la estación por número
        const estacion = estaciones.find(e => e.numero === numero);
        
        if (!estacion) {
            throw new Error(`Estación con número ${numero} no encontrada`);
        }
        
        // Usar el campo estacionId (con e minúscula)
        const estacionId = estacion.estacionId;
        
        if (!estacionId) {
            console.error('❌ estacionId no encontrado en:', estacion);
            throw new Error('No se pudo obtener el estacionId de la estación');
        }
        
        console.log(`🎯 Eliminando estación número ${numero} con estacionId ${estacionId}`);
        
        const response = await fetch(`${API_BASE_URL}/${estacionId}`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(errorText || 'Error al eliminar la estación');
        }

        console.log('✅ Estación eliminada exitosamente');
        showAlert('✅ Estación eliminada exitosamente');
        loadEstaciones();
        
        // Limpiar resultados de búsqueda
        document.getElementById('deleteSearchResults').innerHTML = '';
        document.getElementById('deleteSearchInput').value = '';
        
    } catch (error) {
        console.error('❌ Error al eliminar:', error);
        showAlert('❌ Error al eliminar la estación: ' + error.message, 'error');
    }
}

/**
 * Confirmar eliminación de estación
 * @param {number} numero - Número de estación
 * @param {string} nombre - Nombre de estación
 */
function confirmDelete(numero, nombre) {
    const confirmMessage = `⚠️ ¿Estás seguro de que deseas eliminar la estación ${numero} - ${nombre}?\n\nEsta acción no se puede deshacer.`;
    
    if (confirm(confirmMessage)) {
        deleteEstacion(numero);
    } else {
        console.log('🚫 Eliminación cancelada por el usuario');
    }
}


// ===========================
// RENDERIZADO DE DATOS
// ===========================

/**
 * Renderizar la tabla de estaciones usando EstacionId
 * @param {Array} data - Array de estaciones
 */
function renderEstacionesTable(data) {
    const tableBody = document.getElementById('estacionesTableBody');
    
    if (!data || data.length === 0) {
        tableBody.innerHTML = `
            <tr>
                <td colspan="5" style="text-align: center; padding: 40px; color: #666;">
                    🏜️ No se encontraron estaciones
                </td>
            </tr>`;
        return;
    }

    tableBody.innerHTML = data.map(estacion => `
        <tr>
            <td><strong>${estacion.numero}</strong></td>
            <td>${estacion.nombre}</td>
            <td>
                <span class="status-badge ${estacion.disponible ? 'status-disponible' : 'status-ocupada'}">
                    ${estacion.disponible ? '✅ Disponible' : '❌ Ocupada'}
                </span>
            </td>
            <td>${estacion.turno}</td>
            <td>
                <button class="action-btn btn-edit" onclick="loadEstacionForUpdate(${estacion.estacionId}, '${estacion.nombre.replace(/'/g, "\\'")}')">
                    ✏️ Editar
                </button>
                <button class="action-btn btn-delete" onclick="confirmDelete(${estacion.numero}, '${estacion.nombre.replace(/'/g, "\\'")}')">
                    🗑️ Eliminar
                </button>
            </td>
        </tr>
    `).join('');
    
    console.log(`📊 Tabla renderizada con ${data.length} estaciones`);
}

/**
 * Cargar datos de estación para actualizar usando estacionId
 * @param {number} estacionId - estacionId real de la estación
 * @param {string} nombre - Nombre de estación
 */
async function loadEstacionForUpdate(estacionId, nombre) {
    console.log(`📝 Cargando estación estacionId ${estacionId} para actualizar...`);
    
    try {
        // Buscar la estación por estacionId en nuestra lista local
        const estacion = estaciones.find(e => e.estacionId === estacionId);
        
        if (!estacion) {
            throw new Error('Estación no encontrada');
        }

        // Llenar el formulario con los datos existentes
        document.getElementById('updateId').value = estacionId;
        document.getElementById('updateNumero').value = estacion.numero;
        document.getElementById('updateNombre').value = estacion.nombre;
        document.getElementById('updateTurno').value = estacion.turno || 'Mañana';
        document.getElementById('updateDisponible').checked = estacion.disponible;

        // Mostrar el formulario
        document.getElementById('updateForm').style.display = 'block';
        showSection('update');
        
        console.log('✅ Formulario de actualización cargado con estacionId:', estacionId);
        
    } catch (error) {
        console.error('❌ Error al cargar estación:', error);
        showAlert('❌ Error al cargar la estación: ' + error.message, 'error');
    }
}


// ===========================
// FUNCIONES DE BÚSQUEDA
// ===========================

/**
 * Buscar estaciones en la tabla principal
 */
function searchEstaciones() {
    const searchTerm = document.getElementById('searchInput').value.toLowerCase().trim();
    
    if (!searchTerm) {
        renderEstacionesTable(estaciones);
        return;
    }
    
    const filteredEstaciones = estaciones.filter(estacion => 
        estacion.nombre.toLowerCase().includes(searchTerm) ||
        estacion.numero.toString().includes(searchTerm) ||
        estacion.turno.toLowerCase().includes(searchTerm)
    );
    
    renderEstacionesTable(filteredEstaciones);
    console.log(`🔍 Búsqueda: "${searchTerm}" - ${filteredEstaciones.length} resultados`);
}

/**
 * Buscar estación para actualizar
 */
async function searchForUpdate() {
    const searchTerm = document.getElementById('updateSearchInput').value.trim();
    
    if (searchTerm.length < 2) {
        document.getElementById('updateForm').style.display = 'none';
        return;
    }

    try {
        // Buscar en las estaciones ya cargadas primero
        const foundEstacion = estaciones.find(e => 
            e.nombre.toLowerCase().includes(searchTerm.toLowerCase()) ||
            e.numero.toString() === searchTerm
        );

        if (foundEstacion) {
            loadEstacionForUpdate(foundEstacion.numero, foundEstacion.nombre);
            return;
        }

        // Si no se encuentra localmente, buscar en la API
        const response = await fetch(`${API_BASE_URL}/GetByNombre/${encodeURIComponent(searchTerm)}`);
        
        if (response.ok) {
            const estacionesFound = await response.json();
            if (estacionesFound.length > 0) {
                loadEstacionForUpdate(estacionesFound[0].numero, estacionesFound[0].nombre);
            }
        } else {
            document.getElementById('updateForm').style.display = 'none';
            console.log('🔍 Estación no encontrada');
        }
    } catch (error) {
        console.log('🔍 Error en búsqueda de actualización:', error);
        document.getElementById('updateForm').style.display = 'none';
    }
}

/**
 * Buscar estaciones para eliminar usando estacionId
 */
function searchForDelete() {
    const searchTerm = document.getElementById('deleteSearchInput').value.toLowerCase().trim();
    
    if (searchTerm.length < 2) {
        document.getElementById('deleteSearchResults').innerHTML = '';
        return;
    }

    const filteredEstaciones = estaciones.filter(estacion => 
        estacion.nombre.toLowerCase().includes(searchTerm) ||
        estacion.numero.toString().includes(searchTerm)
    );

    const resultsHTML = filteredEstaciones.map(estacion => `
        <div style="background: white; padding: 15px; margin: 10px 0; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);">
            <div style="display: flex; justify-content: space-between; align-items: center;">
                <div>
                    <strong>Estación ${estacion.numero}</strong> - ${estacion.nombre}<br>
                    <small style="color: #666;">estacionId: ${estacion.estacionId} | ${estacion.turno} | ${estacion.disponible ? '✅ Disponible' : '❌ Ocupada'}</small>
                </div>
                <button class="action-btn btn-delete" onclick="confirmDelete(${estacion.numero}, '${estacion.nombre.replace(/'/g, "\\'")}')">
                    🗑️ Eliminar
                </button>
            </div>
        </div>
    `).join('');

    document.getElementById('deleteSearchResults').innerHTML = resultsHTML || 
        '<p style="text-align: center; color: #666; padding: 20px;">🔍 No se encontraron estaciones</p>';
        
    console.log(`🔍 Búsqueda eliminación: "${searchTerm}" - ${filteredEstaciones.length} resultados`);
}

// ===========================
// FUNCIONES DE CONFIRMACIÓN
// ===========================
/**
 * Cancelar actualización y ocultar formulario
 */
function cancelUpdate() {
    document.getElementById('updateForm').style.display = 'none';
    document.getElementById('updateSearchInput').value = '';
    console.log('❌ Actualización cancelada');
}
/**
 * Confirmar eliminación de estación
 * @param {number} numero - Número de estación
 * @param {string} nombre - Nombre de estación
 */
function confirmDelete(numero, nombre) {
    const confirmMessage = `⚠️ ¿Estás seguro de que deseas eliminar la estación ${numero} - ${nombre}?\n\nEsta acción no se puede deshacer.`;
    
    if (confirm(confirmMessage)) {
        deleteEstacion(numero);
    } else {
        console.log('🚫 Eliminación cancelada por el usuario');
    }
}

// ===========================
// UTILIDADES
// ===========================

/**
 * Resetear formulario a su estado inicial
 * @param {string} formId - ID del formulario a resetear
 */
function resetForm(formId) {
    const form = document.getElementById(formId);
    form.reset();
    
    // Configurar valores por defecto
    if (formId === 'createForm') {
        document.getElementById('createDisponible').checked = true;
        document.getElementById('createTurno').value = 'Mañana';
    }
    
    console.log(`🔄 Formulario ${formId} reseteado`);
}


window.addEventListener('error', function(event) {
    console.error('❌ Error no capturado:', event.error);
    showAlert('Ocurrió un error inesperado. Revisa la consola para más detalles.', 'error');
});


window.addEventListener('unhandledrejection', function(event) {
    console.error('❌ Promesa rechazada no capturada:', event.reason);
    showAlert('Error de conexión. Verifica que la API esté funcionando.', 'error');
});


console.log(`
🚉 Sistema de Gestión de Estaciones
📡 API URL: ${API_BASE_URL}
🕒 Inicializado: ${new Date().toLocaleString()}
📋 Funciones disponibles:
   - loadEstaciones()
   - createEstacion(event)
   - updateEstacion(event)
   - deleteEstacion(numero)
   - searchEstaciones()
   - showSection(section)
`);