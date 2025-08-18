const API_CONFIG = {
    baseURL: 'https://localhost:7044/api',
    endpoints: {
        register: '/Usuario/Usuario',
        login: '/Login/login'
    }
};

let isLoading = false;

// Función para mostrar diferentes páginas
function showPage(pageId) {
    const pages = document.querySelectorAll('.page');
    pages.forEach(page => {
        page.classList.remove('active');
    });
    document.getElementById(pageId).classList.add('active');
    clearForms();
}

// ===========================================
// FUNCIONES DE UTILIDAD
// ===========================================

function clearForms() {
    document.getElementById('registerForm').reset();
    document.getElementById('loginForm').reset();
    clearMessages();
}

function showMessage(message, type = 'success') {
    clearMessages();
    
    const messageDiv = document.createElement('div');
    messageDiv.className = type === 'success' ? 'success-message' : 'error-message';
    messageDiv.textContent = message;
    
    const activeForm = document.querySelector('.page.active form');
    if (activeForm) {
        activeForm.insertBefore(messageDiv, activeForm.firstChild);
    }
}

function clearMessages() {
    const messages = document.querySelectorAll('.success-message, .error-message');
    messages.forEach(msg => msg.remove());
}

// ===========================================
// FUNCIONES DE VALIDACIÓN
// ===========================================

function validateRegisterForm(formData) {
    if (formData.contrasena !== formData.confirmarContrasena) {
        showMessage('Las contraseñas no coinciden', 'error');
        return false;
    }

    if (formData.contrasena.length < 6) {
        showMessage('La contraseña debe tener al menos 6 caracteres', 'error');
        return false;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(formData.email)) {
        showMessage('Por favor ingrese un email válido', 'error');
        return false;
    }

    if (!formData.nombre.trim()) {
        showMessage('El nombre es requerido', 'error');
        return false;
    }

    if (!formData.apellido.trim()) {
        showMessage('El apellido es requerido', 'error');
        return false;
    }

    return true;
}

function validateLoginForm(formData) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(formData.email)) {
        showMessage('Por favor ingrese un email válido', 'error');
        return false;
    }

    if (!formData.contrasena || formData.contrasena.length < 6) {
        showMessage('La contraseña debe tener al menos 6 caracteres', 'error');
        return false;
    }

    return true;
}

// ===========================================
// FUNCIONES DE API
// ===========================================

async function apiRequest(endpoint, method, data = null) {
    try {
        const config = {
            method: method,
            headers: {
                'Content-Type': 'application/json',
            }
        };

        if (data) {
            config.body = JSON.stringify(data);
        }

        console.log(`📤 Enviando ${method} a: ${API_CONFIG.baseURL + endpoint}`);
        console.log('📤 Payload enviado:', data);

        const response = await fetch(API_CONFIG.baseURL + endpoint, config);

        const contentType = response.headers.get('content-type');
        let responseData;

        if (contentType && contentType.includes('application/json')) {
            responseData = await response.json();
        } else {
            responseData = { message: await response.text() };
        }

        console.log('📥 Respuesta recibida:', responseData);

        return {
            ok: response.ok,
            status: response.status,
            data: responseData
        };
    } catch (error) {
        console.error('❌ Error en la petición:', error);
        return {
            ok: false,
            status: 0,
            data: { message: 'Error de conexión' }
        };
    }
}

// Función para registrar usuario
async function registerUser(userData) {
    if (isLoading) return;
    isLoading = true;
    
    showMessage('Registrando usuario...', 'success');
    
    try {
        const result = await apiRequest(API_CONFIG.endpoints.register, 'POST', userData);

        if (result.ok) {
            showMessage('¡Usuario registrado exitosamente! 🎉', 'success');

            // Detectar si la respuesta es un array o un objeto
            let usuario;
            let rol;

            if (Array.isArray(result.data)) {
                usuario = result.data[0];
                rol = usuario?.rol;
            } else {
                usuario = result.data;
                rol = result.data?.rol;
            }

            // Guardar datos del usuario en sessionStorage
            if (usuario) {
                sessionStorage.setItem('userData', JSON.stringify(usuario));
            }

            console.log("✅ Usuario registrado:", usuario);
            console.log("🎭 Rol recibido:", rol);

            // Mostrar mensaje y redirigir según el rol
            if (typeof rol === 'string') {
                const rolNormalizado = rol.trim().toLowerCase();
                
                mostrarMensajeExito(`Registro exitoso como ${rol}.`);

                setTimeout(() => {
                    console.log(`🚀 Intentando redirección para rol: ${rolNormalizado}`);
                    
                    switch (rolNormalizado) {
                        case 'admin':
                            console.log("🚀 Redirigiendo a Admin.html...");
                            window.location.href = './Admin.html';
                            break;
                        case 'usuario':
                            console.log("🚀 Redirigiendo a Usuario.html...");
                            window.location.href = '/Usuario.html';
                            break;
                        default:
                            console.warn(`⚠️ Rol desconocido: ${rolNormalizado}`);
                            showMessage(`⚠️ Rol desconocido: ${rolNormalizado}`, 'error');
                    }
                }, 2000);
            } else {
                console.error('❌ No se pudo determinar el rol del usuario');
                showMessage('❌ No se pudo determinar el rol del usuario', 'error');
            }

        } else {
            const errorMessage = result.data.message || 'Error al registrar usuario';
            console.error('❌ Error en registro:', errorMessage);
            showMessage(errorMessage, 'error');
        }
    } catch (error) {
        console.error("💥 Error en registerUser:", error);
        showMessage('💥 Error inesperado al registrar usuario', 'error');
    } finally {
        isLoading = false;
    }
}

// Función para iniciar sesión
async function loginUser(loginData) {
    if (isLoading) return;
    isLoading = true;
    
    showMessage('Iniciando sesión...', 'success');

    try {
        const result = await apiRequest(API_CONFIG.endpoints.login, 'POST', loginData);

        if (result.ok) {
            showMessage('¡Bienvenido! 🚀', 'success');

            // Guardar token y datos del usuario
            if (result.data.token) {
                sessionStorage.setItem('authToken', result.data.token);
            }

           sessionStorage.setItem('userData', JSON.stringify(result.data));



            const rol = result.data?.rol;

            console.log("✅ Login exitoso. Rol recibido:", rol);

            if (typeof rol === 'string') {
                const rolNormalizado = rol.trim().toLowerCase();
                console.log("🎭 Rol normalizado:", rolNormalizado);

                // Pequeña pausa antes de redirección para que se vea el mensaje
                setTimeout(() => {
                    switch (rolNormalizado) {
                        case 'admin':
                            console.log("🚀 Redirigiendo a Admin.html...");
                            window.location.href = './Admin.html';
                            break;
                        case 'usuario':
                            console.log("🚀 Redirigiendo a dashboard.html...");
                            window.location.href = './dashboard.html';
                            break;
                        default:
                            console.warn(`⚠️ Rol desconocido: ${rolNormalizado}`);
                            showMessage(`⚠️ Rol desconocido: ${rolNormalizado}`, 'error');
                    }
                }, 1000);
            } else {
                console.error("❌ No se pudo determinar el rol del usuario");
                showMessage('❌ No se pudo determinar el rol del usuario', 'error');
            }
        } else {
            console.error('❌ Error en login:', result.data);
            showMessage('❌ Credenciales inválidas.', 'error');
        }
    } catch (error) {
        console.error("💥 Error en loginUser:", error);
        showMessage('💥 Error inesperado al iniciar sesión', 'error');
    } finally {
        isLoading = false;
    }
}

function mostrarMensajeExito(texto) {
    const mensaje = document.getElementById("registro-exitoso");
    if (mensaje) {
        mensaje.textContent = texto;
        mensaje.style.display = "block";
        console.log("✅ Mensaje visual mostrado:", mensaje.textContent);
    } else {
        console.warn("⚠️ Elemento #registro-exitoso no encontrado.");
    }
}

// ===========================================
// EVENT LISTENERS
// ===========================================

document.addEventListener('DOMContentLoaded', function() {
    
    // Manejar envío del formulario de registro
    document.getElementById('registerForm').addEventListener('submit', async function(e) {
        e.preventDefault();
        
        if (isLoading) {
            console.log("⚠️ Ya hay una operación en progreso");
            return;
        }
        
        const formData = {
            nombre: document.getElementById('nombre').value.trim(),
            apellido: document.getElementById('apellido').value.trim(),
            email: document.getElementById('email').value.trim().toLowerCase(),
            contrasena: document.getElementById('contrasena').value,
            confirmarContrasena: document.getElementById('confirmarContrasena').value,
            rol: document.getElementById('rol').value
        };

        if (!validateRegisterForm(formData)) {
            return;
        }

        const userData = {
            Nombre: formData.nombre,
            Apellido: formData.apellido,
            Email: formData.email,
            ContrasenaHash: formData.contrasena,
            Rol: formData.rol
        };

        await registerUser(userData);
    });

    // Manejar envío del formulario de login
    document.getElementById('loginForm').addEventListener('submit', async function(e) {
        e.preventDefault();

        if (isLoading) {
            console.log("⚠️ Ya hay una operación en progreso");
            return;
        }

        const formData = {
            email: document.getElementById('loginEmail').value.trim(),
            contrasena: document.getElementById('loginContrasena').value
        };

        if (!validateLoginForm(formData)) {
            return;
        }

        const loginData = {
            email: formData.email,
            contrasenaHash: formData.contrasena
        };

        await loginUser(loginData);
    });

    // Efectos visuales para los inputs
    document.querySelectorAll('input').forEach(input => {
        input.addEventListener('focus', function() {
            this.parentElement.style.transform = 'translateY(-2px)';
        });
        
        input.addEventListener('blur', function() {
            this.parentElement.style.transform = 'translateY(0)';
        });
    });

    // Limpiar mensajes cuando el usuario empiece a escribir
    document.querySelectorAll('input').forEach(input => {
        input.addEventListener('input', function() {
            clearMessages();
        });
    });
});

// ===========================================
// FUNCIONES ADICIONALES
// ===========================================

function isAuthenticated() {
    return sessionStorage.getItem('authToken') !== null;
}

function getUserData() {
    const userData = sessionStorage.getItem('userData');
    return userData ? JSON.parse(userData) : null;
}

function logout() {
    sessionStorage.removeItem('authToken');
    sessionStorage.removeItem('userData');
    window.location.href = './index.html';
}

function getAuthToken() {
    return sessionStorage.getItem('authToken');
}

// ===========================================
// MANEJO DE ERRORES GLOBALES
// ===========================================

window.addEventListener('error', function(e) {
    console.error('Error global:', e.error);
    showMessage('Ha ocurrido un error inesperado', 'error');
});