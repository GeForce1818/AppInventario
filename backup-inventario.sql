--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2024-11-16 14:30:56

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE stockmanagerdb;
--
-- TOC entry 4973 (class 1262 OID 42376)
-- Name: stockmanagerdb; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE stockmanagerdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Spain.1252';


ALTER DATABASE stockmanagerdb OWNER TO postgres;

\connect stockmanagerdb

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 247 (class 1255 OID 133033)
-- Name: actualizar_producto(character varying, character varying, text, numeric, integer, character varying, character varying, date, date, character varying, integer, character varying, integer, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.actualizar_producto(IN p_producto_id character varying, IN p_nombre character varying, IN p_descripcion text, IN p_precio_unitario numeric, IN p_categoria_id integer, IN p_inventario_id character varying, IN p_lote_id character varying, IN p_fecha_fabricacion date, IN p_fecha_vencimiento date, IN p_lote_ubicacion character varying, IN p_lote_estado integer, IN p_stock_id character varying, IN p_cantidad_disponible integer, IN p_stock_estado integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Bloque para manejo de excepciones
    BEGIN
        -- Actualizar en la tabla producto
        UPDATE public.producto
        SET nombre = p_nombre, 
            descripcion = p_descripcion,
            preciounitario = p_precio_unitario,
            categoria_id = p_categoria_id,
            inventario_id = p_inventario_id
        WHERE producto_id = p_producto_id;

        -- Actualizar en la tabla lote
        UPDATE public.lote
        SET fecha_fabricacion = p_fecha_fabricacion,
            fecha_vencimiento = p_fecha_vencimiento,
            lote_ubicacion = p_lote_ubicacion,
            lote_estado = p_lote_estado
        WHERE lote_id = p_lote_id
        AND producto_id = p_producto_id;

        -- Actualizar en la tabla stock
        UPDATE public.stock
        SET cantidad_disponible = p_cantidad_disponible,
            stock_estado = p_stock_estado
        WHERE stock_id = p_stock_id
        AND lote_id = p_lote_id;

        -- Emitir un mensaje de éxito
        RAISE NOTICE 'Producto % ha sido actualizado correctamente con el lote % y stock %', p_producto_id, p_lote_id, p_stock_id;

    EXCEPTION
        WHEN OTHERS THEN
            -- Emitir un mensaje de error si algo falla
            RAISE EXCEPTION 'Error al actualizar el producto %: %', p_producto_id, SQLERRM;
    END;

END;
$$;


ALTER PROCEDURE public.actualizar_producto(IN p_producto_id character varying, IN p_nombre character varying, IN p_descripcion text, IN p_precio_unitario numeric, IN p_categoria_id integer, IN p_inventario_id character varying, IN p_lote_id character varying, IN p_fecha_fabricacion date, IN p_fecha_vencimiento date, IN p_lote_ubicacion character varying, IN p_lote_estado integer, IN p_stock_id character varying, IN p_cantidad_disponible integer, IN p_stock_estado integer) OWNER TO postgres;

--
-- TOC entry 232 (class 1255 OID 108152)
-- Name: actualizar_stock(character varying, character varying, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.actualizar_stock(IN p_producto_id character varying, IN p_lote_id character varying, IN p_cantidad_a_agregar integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Verifica si existe un registro para el producto y lote especificado
    IF EXISTS (
        SELECT 1 
        FROM public.stock 
        WHERE producto_id = p_producto_id AND lote_id = p_lote_id
    ) THEN
        -- Actualiza la cantidad disponible sumando la nueva cantidad recibida
        UPDATE public.stock
        SET cantidad_disponible = cantidad_disponible + p_cantidad_a_agregar
        WHERE producto_id = p_producto_id AND lote_id = p_lote_id;
		
		COMMIT;

        -- Emitir un mensaje de éxito
        RAISE NOTICE 'La cantidad de stock del producto % (lote %) ha sido actualizada correctamente', p_producto_id, p_lote_id;

    ELSE
        -- Si no existe el producto o el lote, emitir un error
        RAISE EXCEPTION 'No se encontró el producto % con lote % en el stock', p_producto_id, p_lote_id;
    END IF;
END;
$$;


ALTER PROCEDURE public.actualizar_stock(IN p_producto_id character varying, IN p_lote_id character varying, IN p_cantidad_a_agregar integer) OWNER TO postgres;

--
-- TOC entry 246 (class 1255 OID 116560)
-- Name: agregar_producto(character varying, character varying, text, numeric, integer, character varying, character varying, date, date, character varying, integer, character varying, integer, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.agregar_producto(IN p_producto_id character varying, IN p_nombre character varying, IN p_descripcion text, IN p_precio_unitario numeric, IN p_categoria_id integer, IN p_inventario_id character varying, IN p_lote_id character varying, IN p_fecha_fabricacion date, IN p_fecha_vencimiento date, IN p_lote_ubicacion character varying, IN p_lote_estado integer, IN p_stock_id character varying, IN p_cantidad_disponible integer, IN p_stock_estado integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Bloque para manejo de excepciones
    BEGIN
        -- Insertar en la tabla producto
        INSERT INTO public.producto (producto_id, nombre, descripcion, preciounitario, categoria_id, inventario_id)
        VALUES (p_producto_id, p_nombre, p_descripcion, p_precio_unitario, p_categoria_id, p_inventario_id);

        -- Insertar en la tabla lote
        INSERT INTO public.lote (lote_id, producto_id, fecha_fabricacion, fecha_vencimiento, lote_ubicacion, lote_estado)
        VALUES (p_lote_id, p_producto_id, p_fecha_fabricacion, p_fecha_vencimiento, p_lote_ubicacion, p_lote_estado);

        -- Insertar en la tabla stock
        INSERT INTO public.stock (stock_id, lote_id, cantidad_disponible, stock_estado)
        VALUES (p_stock_id, p_lote_id, p_cantidad_disponible, p_stock_estado);

        -- Emitir un mensaje de éxito
        RAISE NOTICE 'Producto % ha sido ingresado correctamente con el lote % y stock %', p_producto_id, p_lote_id, p_stock_id;

    EXCEPTION
        WHEN OTHERS THEN
            -- Emitir un mensaje de error si algo falla
            RAISE EXCEPTION 'Error al insertar el producto %: %', p_producto_id, SQLERRM;
    END;

END;
$$;


ALTER PROCEDURE public.agregar_producto(IN p_producto_id character varying, IN p_nombre character varying, IN p_descripcion text, IN p_precio_unitario numeric, IN p_categoria_id integer, IN p_inventario_id character varying, IN p_lote_id character varying, IN p_fecha_fabricacion date, IN p_fecha_vencimiento date, IN p_lote_ubicacion character varying, IN p_lote_estado integer, IN p_stock_id character varying, IN p_cantidad_disponible integer, IN p_stock_estado integer) OWNER TO postgres;

--
-- TOC entry 233 (class 1255 OID 133034)
-- Name: eliminarproductocompleto(character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.eliminarproductocompleto(IN p_producto_id character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Eliminar los Stocks asociados a los Lotes del Producto
    DELETE FROM public.stock WHERE lote_id IN (SELECT lote_id FROM lote WHERE producto_id = p_producto_id);

    -- Eliminar los Lotes del Producto
    DELETE FROM public.lote WHERE producto_id = p_producto_id;

    -- Eliminar el Producto
    DELETE FROM public.producto WHERE producto_id = p_producto_id;
END;
$$;


ALTER PROCEDURE public.eliminarproductocompleto(IN p_producto_id character varying) OWNER TO postgres;

--
-- TOC entry 245 (class 1255 OID 108138)
-- Name: verificar_stock_bajo(character varying, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.verificar_stock_bajo(IN p_producto_id character varying, IN p_lote_id character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE
    v_lote_ids VARCHAR[];       -- Lista de ID de lotes asociados al producto
    v_cantidad_disponible INT;  -- Cantidad de stock disponible
    v_limite INT := 10;         -- Límite de stock fijo
BEGIN
    -- Validar que los parámetros no sean NULL
    IF p_producto_id IS NULL OR p_lote_id IS NULL THEN
        RAISE EXCEPTION 'Los parámetros p_producto_id y p_lote_id no pueden ser NULL';
    END IF;

    -- Obtener los IDs de lote asociados al producto proporcionado
    SELECT ARRAY_AGG(lote_id)
    INTO v_lote_ids
    FROM public.lote
    WHERE producto_id = p_producto_id;

    -- Verificar si existen lotes asociados al producto
    IF v_lote_ids IS NULL THEN
        RAISE EXCEPTION 'No se encontraron lotes asociados al Producto ID: %', p_producto_id;
    END IF;

    -- Verificar si el lote proporcionado está en la lista de lotes asociados
    IF NOT (p_lote_id = ANY(v_lote_ids)) THEN
        RAISE EXCEPTION 'El Lote ID: % no está asociado al Producto ID: %', p_lote_id, p_producto_id;
    END IF;

    -- Consultar la cantidad de stock disponible en la tabla stock para el lote proporcionado
    SELECT cantidad_disponible
    INTO v_cantidad_disponible
    FROM public.stock
    WHERE lote_id = p_lote_id;

    -- Verificar si el stock es menor o igual al límite
    IF v_cantidad_disponible <= v_limite THEN
        RAISE NOTICE 'Stock bajo: Producto ID: %, Lote ID: %. Cantidad actual: %, se requiere reposición.',
                     p_producto_id, p_lote_id, v_cantidad_disponible;
    ELSE
        RAISE NOTICE 'Stock suficiente: Producto ID: %, Lote ID: %. Cantidad actual: %.',
                     p_producto_id, p_lote_id, v_cantidad_disponible;
    END IF;

EXCEPTION
    -- Manejar el caso donde no se encuentra información
    WHEN NO_DATA_FOUND THEN
        RAISE NOTICE 'No se encontró información para el Producto ID: % y Lote ID: %.', p_producto_id, p_lote_id;
    -- Manejar cualquier otro error
    WHEN OTHERS THEN
        RAISE NOTICE 'Ocurrió un error: %. Verifica los datos ingresados.', SQLERRM;
END;
$$;


ALTER PROCEDURE public.verificar_stock_bajo(IN p_producto_id character varying, IN p_lote_id character varying) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 42391)
-- Name: categoria; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.categoria (
    categoria_id integer NOT NULL,
    categoria_nombre character varying(100),
    descripcion character varying(255)
);


ALTER TABLE public.categoria OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 116457)
-- Name: detalle_pedido; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.detalle_pedido (
    pedido_id character varying(100) NOT NULL,
    producto_id character varying(100) NOT NULL,
    cantidad_solicitada integer,
    lote_id character varying(100),
    fecha_fabricacion date,
    fecha_vencimiento date,
    precio_unitario numeric(10,2)
);


ALTER TABLE public.detalle_pedido OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 116539)
-- Name: detalle_pedido_nuevo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.detalle_pedido_nuevo (
    pedido_nuevo_id character varying(100) NOT NULL,
    producto_nuevo_id character varying(100) NOT NULL,
    lote_id character varying(100),
    cantidad_solicitada integer,
    precio_unitario numeric(10,2),
    fecha_fabricacion date,
    fecha_vencimiento date
);


ALTER TABLE public.detalle_pedido_nuevo OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 42404)
-- Name: estado_lote; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.estado_lote (
    estado_id integer NOT NULL,
    estado_nombre character varying(100)
);


ALTER TABLE public.estado_lote OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 42431)
-- Name: estado_stock; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.estado_stock (
    estado_id integer NOT NULL,
    estado_nombre character varying(100)
);


ALTER TABLE public.estado_stock OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 141266)
-- Name: extraccion_producto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.extraccion_producto (
    extraccion_id character varying(100) NOT NULL,
    producto_id character varying(50) NOT NULL,
    lote_id character varying(50) NOT NULL,
    cantidad_extraida integer NOT NULL,
    fecha_extraccion date NOT NULL
);


ALTER TABLE public.extraccion_producto OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 42492)
-- Name: inventario; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.inventario (
    inventario_id character varying(255) NOT NULL,
    inventario_nombre character varying(100)
);


ALTER TABLE public.inventario OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 42416)
-- Name: lote; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.lote (
    lote_id character varying(50) NOT NULL,
    producto_id character varying(50) NOT NULL,
    fecha_fabricacion date,
    fecha_vencimiento date,
    lote_ubicacion character varying(200),
    lote_estado integer
);


ALTER TABLE public.lote OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 116442)
-- Name: pedido; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.pedido (
    pedido_id character varying(100) NOT NULL,
    pedido_fecha date,
    proveedor_id character varying(100),
    fecha_entrega date,
    tipo_pago_id character varying(100)
);


ALTER TABLE public.pedido OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 116489)
-- Name: pedido_nuevo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.pedido_nuevo (
    pedido_nuevo_id character varying(100) NOT NULL,
    pedido_nuevo_fecha date NOT NULL,
    proveedor_id character varying(100),
    fecha_entrega date,
    tipo_pago_id character varying(100)
);


ALTER TABLE public.pedido_nuevo OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 42396)
-- Name: producto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.producto (
    producto_id character varying(50) NOT NULL,
    nombre character varying(100),
    descripcion character varying(255),
    preciounitario numeric(10,2),
    categoria_id integer,
    inventario_id character varying(255)
);


ALTER TABLE public.producto OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 116524)
-- Name: producto_nuevo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.producto_nuevo (
    producto_nuevo_id character varying(100) NOT NULL,
    nombre character varying(100),
    descripcion character varying(255),
    precio_estimado numeric(10,2),
    categoria_id integer,
    estado_registro integer
);


ALTER TABLE public.producto_nuevo OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 42384)
-- Name: proveedor; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.proveedor (
    proveedor_id character varying(50) NOT NULL,
    proveedor_nombre character varying(100),
    numerotelefono character varying(15),
    email character varying(100),
    direccion character varying(255)
);


ALTER TABLE public.proveedor OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 116519)
-- Name: registro; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.registro (
    registro_id integer NOT NULL,
    estado character varying(100)
);


ALTER TABLE public.registro OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 42450)
-- Name: stock; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.stock (
    stock_id character varying(255) NOT NULL,
    lote_id character varying(50),
    cantidad_disponible integer,
    stock_estado integer
);


ALTER TABLE public.stock OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 116437)
-- Name: tipo_pago; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tipo_pago (
    tipo_pago_id character varying(100) NOT NULL,
    descripcion character varying(255) NOT NULL
);


ALTER TABLE public.tipo_pago OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 42377)
-- Name: usuario; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuario (
    usuario_id integer NOT NULL,
    nombre character varying(200),
    apellido character varying(200),
    numerotelefono character varying(15),
    email character varying(100),
    direccion character varying(255),
    contrasena character varying(255)
);


ALTER TABLE public.usuario OWNER TO postgres;

--
-- TOC entry 4953 (class 0 OID 42391)
-- Dependencies: 217
-- Data for Name: categoria; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.categoria (categoria_id, categoria_nombre, descripcion) FROM stdin;
1	Electrónica	Dispositivos electrónicos y accesorios
2	Ropa	Prendas de vestir para todas las edades
3	Alimentos	Productos alimenticios y bebidas
4	Juguetes	Juguetes y juegos para niños
6	Salud	Productos de salud y bienestar
7	Deportes	Equipamiento deportivo
8	Libros	Literatura y libros de texto
9	Accesorios	Accesorios para móviles y computadoras
10	Muebles	Muebles para la casa y oficina
11	Cosméticos	Productos de belleza
12	Herramientas	Herramientas y equipo de trabajo
13	Automóviles	Accesorios y piezas para automóviles
14	Jardinería	Artículos para el cuidado del jardín
15	Tecnología	Nuevas tecnologías y gadgets
16	Relojes	Relojes de pulsera y accesorios
17	Viajes	Equipaje y accesorios de viaje
18	Fotografía	Cámaras y accesorios fotográficos
19	Música	Instrumentos y equipos de música
20	Cine	Películas y series en formato físico
21	Mascotas	Productos para el cuidado de mascotas
22	Suplementos	Suplementos nutricionales
23	Artículos de Oficina	Suministros para oficina
24	Calzado	Zapatos y calzado deportivo
25	Cuidado Personal	Productos de higiene personal
26	Limpieza	Productos de limpieza para el hogar
27	Tecnología Vestible	Dispositivos como smartwatches
28	Arte	Suministros de arte y manualidades
29	Cuidado Infantil	Artículos para bebés y niños
30	Antigüedades	Objetos antiguos y coleccionables
5	Joyeria	Accesorios bonitos y brillantes
\.


--
-- TOC entry 4962 (class 0 OID 116457)
-- Dependencies: 226
-- Data for Name: detalle_pedido; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.detalle_pedido (pedido_id, producto_id, cantidad_solicitada, lote_id, fecha_fabricacion, fecha_vencimiento, precio_unitario) FROM stdin;
PED001	PROD001	50	LOTE001	2024-01-10	2025-01-10	25.50
PED001	PROD002	30	LOTE002	2024-02-15	2025-02-15	18.75
PED002	PROD003	40	LOTE003	2024-03-20	2025-03-20	32.10
PED002	PROD004	60	LOTE004	2024-04-25	2025-04-25	27.80
PED003	PROD005	25	LOTE005	2024-05-30	2025-05-30	19.60
PED003	PROD006	35	LOTE006	2024-06-05	2025-06-05	22.45
PED004	PROD007	55	LOTE007	2024-07-10	2025-07-10	26.75
PED004	PROD008	45	LOTE008	2024-08-15	2025-08-15	24.30
PED005	PROD009	20	LOTE009	2024-09-20	2025-09-20	33.90
PED005	PROD010	65	LOTE010	2024-10-25	2025-10-25	29.00
PED006	PROD011	70	LOTE011	2024-11-05	2025-11-05	31.50
PED006	PROD012	80	LOTE012	2024-12-10	2025-12-10	35.25
PED007	PROD013	90	LOTE013	2024-01-15	2025-01-15	21.80
PED007	PROD014	100	LOTE014	2024-02-20	2025-02-20	23.60
PED008	PROD015	110	LOTE015	2024-03-25	2025-03-25	28.90
PED008	PROD016	120	LOTE016	2024-04-30	2025-04-30	20.70
PED009	PROD017	130	LOTE017	2024-05-05	2025-05-05	34.85
PED009	PROD018	140	LOTE018	2024-06-10	2025-06-10	30.40
PED010	PROD019	150	LOTE019	2024-07-15	2025-07-15	32.65
PED010	PROD020	160	LOTE020	2024-08-20	2025-08-20	27.45
PED011	PROD021	170	LOTE021	2024-09-25	2025-09-25	25.10
PED011	PROD022	180	LOTE022	2024-10-30	2025-10-30	29.85
PED012	PROD023	190	LOTE023	2024-11-05	2025-11-05	33.20
PED012	PROD024	200	LOTE024	2024-12-10	2025-12-10	22.30
PED013	PROD025	210	LOTE025	2024-01-15	2025-01-15	26.95
PED013	PROD026	220	LOTE026	2024-02-20	2025-02-20	28.75
PED014	PROD027	230	LOTE027	2024-03-25	2025-03-25	31.10
PED014	PROD028	240	LOTE028	2024-04-30	2025-04-30	19.80
PED015	PROD029	250	LOTE029	2024-05-05	2025-05-05	35.95
PED015	PROD030	260	LOTE030	2024-06-10	2025-06-10	34.50
PED021	PROD004	15	LOTE035	2024-11-30	2025-02-22	15.00
PED021	PROD031	34		2024-11-29	2025-05-29	399.99
PED022	PROD013	15	LOTE045	2024-11-30	\N	45.00
\.


--
-- TOC entry 4966 (class 0 OID 116539)
-- Dependencies: 230
-- Data for Name: detalle_pedido_nuevo; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.detalle_pedido_nuevo (pedido_nuevo_id, producto_nuevo_id, lote_id, cantidad_solicitada, precio_unitario, fecha_fabricacion, fecha_vencimiento) FROM stdin;
PEDN001	PRODN001	LOTE036	40	850.00	2024-11-01	2028-12-30
PEDN002	PRODN005	LOTE040	15	200.00	\N	\N
\.


--
-- TOC entry 4955 (class 0 OID 42404)
-- Dependencies: 219
-- Data for Name: estado_lote; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.estado_lote (estado_id, estado_nombre) FROM stdin;
4	En producción
5	Vencido
6	Disponible
7	Devuelto
8	En espera
9	En mantenimiento
1	En almacen
2	En tránsito
3	Entregado
10	Descontinuado
11	Obsoleto
12	En revisión
13	Reparado
14	Transferido
15	Cancelado
16	Asignado
17	Esperando recepción
18	Dañado
19	Perdido
20	Recibido
21	Pausado
22	Excedido
23	Aprobado
24	Suspendido
25	Planificado
26	Cerrado
27	Pendiente
28	Vendido
29	No asignado
30	Completado
\.


--
-- TOC entry 4957 (class 0 OID 42431)
-- Dependencies: 221
-- Data for Name: estado_stock; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.estado_stock (estado_id, estado_nombre) FROM stdin;
4	Reabastecido
5	En espera
6	En tránsito
7	Bajo
8	En mantenimiento
9	Vencido
1	Disponible
2	Agotado
3	Reservado
10	Cancelado
11	Descontinuado
12	Obsoleto
13	Devuelto
14	Dañado
15	Perdido
16	Reparado
17	Transferido
18	Asignado
19	Esperando recepción
20	Pausado
21	Excedido
22	Aprobado
23	Suspendido
24	Planificado
25	Cerrado
26	Pendiente
27	Vendido
28	No asignado
29	Completado
30	En revisión
\.


--
-- TOC entry 4967 (class 0 OID 141266)
-- Dependencies: 231
-- Data for Name: extraccion_producto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.extraccion_producto (extraccion_id, producto_id, lote_id, cantidad_extraida, fecha_extraccion) FROM stdin;
EXT002	PROD013	LOTE013	5	2024-10-31
EXT003	PROD001	LOTE001	10	2024-10-31
EXT004	PROD013	LOTE013	20	2024-11-21
\.


--
-- TOC entry 4959 (class 0 OID 42492)
-- Dependencies: 223
-- Data for Name: inventario; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.inventario (inventario_id, inventario_nombre) FROM stdin;
INV001	Centro de Distribución Central
INV002	Centro de Distribución Norte
INV003	Centro de Distribución Sur
INV004	Depósito de Exportación
INV005	Depósito de Materias Peligrosas
INV006	Inventario de Desperdicios
INV007	Inventario de Ofertas
INV008	Inventario de Liquidación
INV009	Inventario de Nuevos Productos
INV010	Inventario de Sucursales
INV011	Inventario de Antigüedades
INV012	Inventario de Artículos de Temporada
INV013	Inventario de Productos en Espera
INV014	Inventario de Productos Vencidos
INV015	Inventario de Productos Regresados
INV016	Inventario de Productos Dañados
INV017	Inventario de Productos Asignados
INV018	Inventario de Productos en Producción
INV019	Inventario de Productos Cancelados
INV020	Inventario de Productos Obsoletos
INV021	Inventario de Productos en Revisión
INV022	Inventario de Productos En Espera
INV023	Inventario de Productos en Mantenimiento
INV024	Inventario de Productos en Tránsito
INV025	Inventario de Productos en Demanda
INV026	Inventario de Productos en Exceso
INV027	Inventario de Productos Especiales
INV028	Inventario de Productos Premium
INV029	Inventario de Productos en Liquidación
INV030	Inventario de Productos para Reabastecer
\.


--
-- TOC entry 4956 (class 0 OID 42416)
-- Dependencies: 220
-- Data for Name: lote; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.lote (lote_id, producto_id, fecha_fabricacion, fecha_vencimiento, lote_ubicacion, lote_estado) FROM stdin;
LOTE031	PROD031	2023-01-01	2025-01-01	Almacén A	4
LOTE002	PROD002	2024-02-01	2025-02-01	Almacén Secundario	1
LOTE003	PROD003	2024-03-01	2025-03-01	Tienda A	1
LOTE004	PROD004	2024-04-01	2025-04-01	Tienda B	1
LOTE005	PROD005	2024-05-01	2025-05-01	Centro de Distribución	1
LOTE032	PROD001	2024-02-01	2025-02-02	Almacen Principal	1
LOTE001	PROD001	2024-01-01	2025-01-01	Almacén Principal	1
LOTE006	PROD006	2024-06-01	2025-06-01	Almacén F	2
LOTE007	PROD007	2024-07-01	2025-07-01	Almacén G	3
LOTE008	PROD008	2024-08-01	2025-08-01	Almacén H	4
LOTE009	PROD009	2024-09-01	2025-09-01	Almacén I	1
LOTE010	PROD010	2024-10-01	2025-10-01	Almacén J	2
LOTE011	PROD011	2024-11-01	2025-11-01	Almacén K	3
LOTE012	PROD012	2024-12-01	2025-12-01	Almacén L	4
LOTE013	PROD013	2024-01-15	2025-01-15	Almacén M	1
LOTE014	PROD014	2024-02-15	2025-02-15	Almacén N	2
LOTE015	PROD015	2024-03-15	2025-03-15	Almacén O	3
LOTE016	PROD016	2024-04-15	2025-04-15	Almacén P	4
LOTE017	PROD017	2024-05-15	2025-05-15	Almacén Q	1
LOTE018	PROD018	2024-06-15	2025-06-15	Almacén R	2
LOTE019	PROD019	2024-07-15	2025-07-15	Almacén S	3
LOTE020	PROD020	2024-08-15	2025-08-15	Almacén T	4
LOTE021	PROD021	2024-09-15	2025-09-15	Almacén U	1
LOTE022	PROD022	2024-10-15	2025-10-15	Almacén V	2
LOTE023	PROD023	2024-11-15	2025-11-15	Almacén W	3
LOTE024	PROD024	2024-12-15	2025-12-15	Almacén X	4
LOTE025	PROD025	2024-01-10	2025-01-10	Almacén Y	1
LOTE026	PROD026	2024-02-10	2025-02-10	Almacén Z	2
LOTE027	PROD027	2024-03-10	2025-03-10	Almacén AA	3
LOTE028	PROD028	2024-04-10	2025-04-10	Almacén AB	4
LOTE029	PROD029	2024-05-10	2025-05-10	Almacén AC	1
LOTE030	PROD030	2024-06-10	2025-06-10	Almacén AD	2
\.


--
-- TOC entry 4961 (class 0 OID 116442)
-- Dependencies: 225
-- Data for Name: pedido; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.pedido (pedido_id, pedido_fecha, proveedor_id, fecha_entrega, tipo_pago_id) FROM stdin;
PED001	2024-10-01	PROV001	2024-10-10	PAGO01
PED002	2024-10-02	PROV002	2024-10-12	PAGO02
PED003	2024-10-03	PROV003	2024-10-15	PAGO01
PED004	2024-10-04	PROV004	2024-10-18	PAGO03
PED005	2024-10-05	PROV005	2024-10-20	PAGO02
PED006	2024-10-06	PROV001	2024-10-25	PAGO03
PED007	2024-10-07	PROV002	2024-10-27	PAGO01
PED008	2024-10-08	PROV003	2024-10-30	PAGO02
PED009	2024-10-09	PROV004	2024-11-02	PAGO03
PED010	2024-10-10	PROV005	2024-11-05	PAGO01
PED011	2024-10-11	PROV001	2024-11-10	PAGO02
PED012	2024-10-12	PROV002	2024-11-15	PAGO03
PED013	2024-10-13	PROV003	2024-11-18	PAGO01
PED014	2024-10-14	PROV004	2024-11-20	PAGO02
PED015	2024-10-15	PROV005	2024-11-25	PAGO03
PED016	2024-10-16	PROV001	2024-11-27	PAGO01
PED017	2024-10-17	PROV002	2024-11-30	PAGO02
PED018	2024-10-18	PROV003	2024-12-02	PAGO03
PED019	2024-10-19	PROV004	2024-12-05	PAGO01
PED020	2024-10-20	PROV005	2024-12-10	PAGO02
PED021	2024-11-06	PROV006	2024-11-21	PAGO02
PED022	2024-11-09	PROV005	2024-11-29	PAGO03
\.


--
-- TOC entry 4963 (class 0 OID 116489)
-- Dependencies: 227
-- Data for Name: pedido_nuevo; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.pedido_nuevo (pedido_nuevo_id, pedido_nuevo_fecha, proveedor_id, fecha_entrega, tipo_pago_id) FROM stdin;
PEDN001	2024-11-07	PROV001	2024-11-29	PAGO03
PEDN002	2024-11-22	PROV008	2024-11-29	PAGO02
\.


--
-- TOC entry 4954 (class 0 OID 42396)
-- Dependencies: 218
-- Data for Name: producto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.producto (producto_id, nombre, descripcion, preciounitario, categoria_id, inventario_id) FROM stdin;
PROD004	Muñeca	Muñeca de juguete para niños	15.00	4	INV004
PROD031	Bocina Mega	Sonido Dolby Surround HD	399.99	1	INV005
PROD002	Camiseta	Camiseta de algodón	20.00	2	INV003
PROD001	Laptop	Laptop de 15 pulgadas con 16GB RAM	1200.00	1	INV001
PROD008	Libro de Cocina	Libro con recetas saludables	10.00	8	INV008
PROD011	Crema Hidratante	Crema para el cuidado de la piel	19.99	11	INV011
PROD012	Taladro	Taladro eléctrico	89.99	12	INV012
PROD013	Aceite de Motor	Aceite sintético para motores	45.00	13	INV013
PROD014	Silla	Silla ergonómica	129.99	10	INV014
PROD016	Reloj	Reloj inteligente	199.99	16	INV016
PROD017	Mochila	Mochila escolar	30.00	7	INV017
PROD018	Cámara	Cámara digital de 24 MP	399.99	18	INV018
PROD019	Guitarra	Guitarra acústica	99.99	19	INV019
PROD020	DVD	Película en DVD	14.99	20	INV020
PROD021	Correa para Mascota	Correa ajustable para perros	10.00	21	INV021
PROD022	Proteína	Proteína en polvo	29.99	22	INV022
PROD023	Papel de Oficina	Paquete de papel para impresora	5.00	23	INV023
PROD024	Zapatillas Deportivas	Zapatillas para correr	69.99	24	INV024
PROD025	Jabón	Jabón natural	3.50	25	INV025
PROD027	Auriculares	Auriculares Bluetooth	59.99	27	INV027
PROD003	Sofá	Sofá de 3 plazas	500.00	10	INV003
PROD005	Cereal	Cereal integral	4.50	3	INV005
PROD006	Juguete de Construcción	Juego de bloques de construcción	25.00	4	INV006
PROD007	Pelota	Pelota de fútbol	15.00	4	INV007
PROD009	Cargador Inalambrico	Cargador inalambrico para dispositivos	39.99	15	INV009
PROD010	Mesa de Comedor	Mesa de madera para comedor	299.99	10	INV010
PROD015	Tenedor	Tenedor de acero inoxidable	5.00	5	INV015
PROD026	Botella	Botella de agua reutilizable	12.00	3	INV026
PROD028	Altavoz	Altavoz inalámbrico	89.99	15	INV028
PROD029	Sábana	Sábana de algodón	25.00	10	INV029
PROD030	Cafetera	Cafetera eléctrica	49.99	15	INV030
\.


--
-- TOC entry 4965 (class 0 OID 116524)
-- Dependencies: 229
-- Data for Name: producto_nuevo; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.producto_nuevo (producto_nuevo_id, nombre, descripcion, precio_estimado, categoria_id, estado_registro) FROM stdin;
PRODN001	Laptop HP	Laptop HP con procesador Intel i5, 8GB RAM, 256GB SSD	850.00	1	2
PRODN002	Monitor Samsung	Monitor Samsung curvo de 27 pulgadas, resolución 4K	300.00	2	3
PRODN003	Teclado Mecánico	Teclado mecánico retroiluminado RGB, conexión USB	80.00	3	4
PRODN004	Mouse Logitech	Mouse inalámbrico Logitech, alta precisión, recargable	45.00	3	5
PRODN005	Impresora Epson	Impresora multifuncional Epson EcoTank con sistema de tinta	200.00	4	2
PRODN006	Tablet Lenovo	Tablet Lenovo de 10 pulgadas, 64GB almacenamiento	150.00	5	3
PRODN008	Auriculares Sony	Auriculares inalámbricos Sony con cancelación de ruido	120.00	6	5
PRODN009	Cámara Canon	Cámara digital Canon con lente de 18-55mm	450.00	7	2
PRODN010	TV LG	Televisor LG Smart de 55 pulgadas, resolución 4K	700.00	2	3
PRODN011	Disco Duro Externo	Disco duro externo de 1TB, USB 3.0	60.00	8	4
PRODN012	Power Bank	Batería portátil de 10000mAh con doble salida USB	30.00	9	5
PRODN013	Bocina Bluetooth	Bocina portátil Bluetooth con sonido estéreo	40.00	9	2
PRODN014	Cable HDMI	Cable HDMI de alta velocidad, 2 metros	10.00	8	3
PRODN015	Reproductor Blu-ray	Reproductor Blu-ray Sony con soporte Full HD	110.00	10	4
PRODN016	Cafetera Oster	Cafetera automática Oster con molino integrado	90.00	11	5
PRODN017	Aspiradora Philips	Aspiradora sin bolsa Philips, con filtro HEPA	130.00	12	2
PRODN018	Parlante JBL	Parlante JBL portátil resistente al agua	60.00	9	3
PRODN019	Ventilador de Torre	Ventilador de torre con control remoto y temporizador	50.00	13	4
PRODN020	Freidora de Aire	Freidora de aire sin aceite, capacidad 3.5L	75.00	14	5
PRODN021	Licuadora Oster	Licuadora de alta potencia, ideal para smoothies	45.00	11	2
PRODN022	Sandwichera Philips	Sandwichera de placas antiadherentes	20.00	11	3
PRODN023	Secadora de Pelo	Secadora de pelo profesional con control de temperatura	35.00	15	4
PRODN024	Reloj Inteligente	Reloj inteligente con monitor de actividad física	80.00	16	5
PRODN025	Cortadora de Césped	Cortadora de césped eléctrica, fácil de maniobrar	100.00	17	2
PRODN026	Termo Stanley	Termo Stanley de acero inoxidable, 1L	25.00	18	3
PRODN027	Guantes de Boxeo	Guantes de boxeo de cuero sintético	30.00	19	4
PRODN028	Tapete de Yoga	Tapete de yoga antideslizante, 6mm de espesor	15.00	20	5
PRODN029	Mochila Táctica	Mochila de 40L, resistente al agua, varios compartimentos	50.00	21	2
PRODN030	Linterna LED	Linterna LED recargable, resistente al agua	20.00	22	3
PRODN007	Smartphone Xiaomi	Smartphone Xiaomi con 6GB RAM, 128GB almacenamiento	250.00	1	4
\.


--
-- TOC entry 4952 (class 0 OID 42384)
-- Dependencies: 216
-- Data for Name: proveedor; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.proveedor (proveedor_id, proveedor_nombre, numerotelefono, email, direccion) FROM stdin;
PROV001	Proveedor A	123456789	contacto@proveedora.com	Calle 123, Ciudad A
PROV002	Proveedor B	987654321	info@proveedorb.com	Avenida 456, Ciudad B
PROV003	Proveedor C	456123789	ventas@proveedorc.com	Boulevard 789, Ciudad C
PROV004	Proveedor D	321654987	soporte@proveedord.com	Plaza 159, Ciudad D
PROV005	Proveedor E	654789123	consultas@proveedore.com	Ruta 753, Ciudad E
PROV006	Proveedor F	678901234	proveedorf@mail.com	Dirección F
PROV007	Proveedor G	789012345	proveedorg@mail.com	Dirección G
PROV008	Proveedor H	890123456	proveedorh@mail.com	Dirección H
PROV009	Proveedor I	901234567	proveedori@mail.com	Dirección I
PROV010	Proveedor J	012345678	proveedorj@mail.com	Dirección J
PROV011	Proveedor K	223344556	proveedork@mail.com	Dirección K
PROV012	Proveedor L	334455667	proveedorl@mail.com	Dirección L
PROV013	Proveedor M	445566778	proveedorm@mail.com	Dirección M
PROV014	Proveedor N	556677889	proveedorn@mail.com	Dirección N
PROV015	Proveedor O	667788990	proveedoro@mail.com	Dirección O
PROV016	Proveedor P	778899001	proveedorp@mail.com	Dirección P
PROV017	Proveedor Q	889900112	proveedorq@mail.com	Dirección Q
PROV018	Proveedor R	990011223	proveedorr@mail.com	Dirección R
PROV019	Proveedor S	101112233	proveedors@mail.com	Dirección S
PROV020	Proveedor T	121314245	proveedort@mail.com	Dirección T
PROV021	Proveedor U	131415256	proveedoru@mail.com	Dirección U
PROV022	Proveedor V	141516267	proveedoru@mail.com	Dirección V
PROV023	Proveedor W	151617278	proveedorw@mail.com	Dirección W
PROV024	Proveedor X	161718289	proveedorx@mail.com	Dirección X
PROV025	Proveedor Y	171819290	proveedory@mail.com	Dirección Y
PROV027	Proveedor AA	192021312	proveedoraa@mail.com	Dirección AA
PROV028	Proveedor AB	202122323	proveedorab@mail.com	Dirección AB
PROV029	Proveedor AC	212223334	proveedorac@mail.com	Dirección AC
PROV030	Proveedor AD	222324345	proveedorad@mail.com	Dirección AD
PROV026	Proveedor Z	181920301	proveedorz@mail.com	Dirección ZZ
PROV031	Proveedor AE	23135465	proveedorae@mail.com	Dirección AE
\.


--
-- TOC entry 4964 (class 0 OID 116519)
-- Dependencies: 228
-- Data for Name: registro; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.registro (registro_id, estado) FROM stdin;
1	registrado
2	no registrado
3	pendiente
4	en revisión
5	rechazado
6	completado
7	cancelado
8	en proceso
9	aprobado
10	finalizado
\.


--
-- TOC entry 4958 (class 0 OID 42450)
-- Dependencies: 222
-- Data for Name: stock; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.stock (stock_id, lote_id, cantidad_disponible, stock_estado) FROM stdin;
STOCK002	LOTE002	140	1
STOCK003	LOTE003	180	1
STOCK004	LOTE004	40	1
STOCK006	LOTE006	25	1
STOCK008	LOTE008	10	3
STOCK009	LOTE009	80	5
STOCK011	LOTE011	55	8
STOCK012	LOTE012	33	2
STOCK014	LOTE014	60	9
STOCK015	LOTE015	90	11
STOCK016	LOTE016	30	25
STOCK017	LOTE017	40	15
STOCK018	LOTE018	22	24
STOCK019	LOTE019	75	4
STOCK020	LOTE020	18	2
STOCK021	LOTE021	25	7
STOCK022	LOTE022	50	9
STOCK023	LOTE023	15	22
STOCK024	LOTE024	40	1
STOCK025	LOTE025	65	23
STOCK026	LOTE026	35	4
STOCK027	LOTE027	20	8
STOCK028	LOTE028	10	1
STOCK029	LOTE029	85	5
STOCK030	LOTE030	5	30
STOCK031	LOTE031	50	5
STOCK005	LOTE005	320	1
STOCK032	LOTE032	50	5
STOCK001	LOTE001	181	1
STOCK007	LOTE007	60	3
STOCK010	LOTE010	15	7
STOCK013	LOTE013	43	4
\.


--
-- TOC entry 4960 (class 0 OID 116437)
-- Dependencies: 224
-- Data for Name: tipo_pago; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tipo_pago (tipo_pago_id, descripcion) FROM stdin;
PAGO01	Transferencia bancaria
PAGO02	Tarjeta de crédito
PAGO03	Efectivo
PAGO04	Cheque
PAGO05	Pago en línea
\.


--
-- TOC entry 4951 (class 0 OID 42377)
-- Dependencies: 215
-- Data for Name: usuario; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.usuario (usuario_id, nombre, apellido, numerotelefono, email, direccion, contrasena) FROM stdin;
1	Juan	Pérez	111222333	juan.perez@example.com	Calle Falsa 123	password1
2	Ana	García	444555666	ana.garcia@example.com	Avenida Siempre Viva 742	password2
3	Luis	Martínez	777888999	luis.martinez@example.com	Boulevard de los Sueños 456	password3
4	María	Lopez	000111222	maria.lopez@example.com	Plaza de la Libertad 789	password4
5	Carlos	Sánchez	333444555	carlos.sanchez@example.com	Ruta 11 321	password5
6	Laura	Hernández	5678901234	laura.hernandez@example.com	Calle 6, Ciudad F	password303
7	Javier	García	6789012345	javier.garcia@example.com	Calle 7, Ciudad G	password404
8	Sofía	Ramírez	7890123456	sofia.ramirez@example.com	Calle 8, Ciudad H	password505
9	David	Fernández	8901234567	david.fernandez@example.com	Calle 9, Ciudad I	password606
10	Isabel	Torres	9012345678	isabel.torres@example.com	Calle 10, Ciudad J	password707
11	Diego	Jiménez	3214567890	diego.jimenez@example.com	Calle 11, Ciudad K	password808
12	Clara	Morales	4325678901	clara.morales@example.com	Calle 12, Ciudad L	password909
13	Ricardo	Vázquez	5436789012	ricardo.vazquez@example.com	Calle 13, Ciudad M	password010
14	Patricia	Castro	6547890123	patricia.castro@example.com	Calle 14, Ciudad N	password111
15	Jorge	Ríos	7658901234	jorge.rios@example.com	Calle 15, Ciudad O	password212
16	Silvia	Salas	8769012345	silvia.salas@example.com	Calle 16, Ciudad P	password313
17	Raúl	Santos	9870123456	raul.santos@example.com	Calle 17, Ciudad Q	password414
18	Elena	Cortez	1234567809	elena.cortez@example.com	Calle 18, Ciudad R	password515
19	Andrés	Bermúdez	2345678910	andres.bermudez@example.com	Calle 19, Ciudad S	password616
20	Nadia	Mendoza	3456789021	nadia.mendoza@example.com	Calle 20, Ciudad T	password717
21	Hugo	Ponce	4567890132	hugo.ponce@example.com	Calle 21, Ciudad U	password818
22	Sonia	Vega	5678901243	sonia.vega@example.com	Calle 22, Ciudad V	password919
23	Gabriel	Núñez	6789012354	gabriel.nunez@example.com	Calle 23, Ciudad W	password020
24	Verónica	Guzmán	7890123465	veronica.guzman@example.com	Calle 24, Ciudad X	password121
25	Fernando	Ceballos	8901234576	fernando.ceballos@example.com	Calle 25, Ciudad Y	password222
26	Estefanía	Díaz	9012345687	estefania.diaz@example.com	Calle 26, Ciudad Z	password323
27	Octavio	Salinas	3214567891	octavio.salinas@example.com	Calle 27, Ciudad AA	password424
28	Marisol	Zamora	4325678902	marisol.zamora@example.com	Calle 28, Ciudad AB	password525
29	Cristian	Alvarado	5436789013	cristian.alvarado@example.com	Calle 29, Ciudad AC	password626
30	Nora	Hidalgo	6547890124	nora.hidalgo@example.com	Calle 30, Ciudad AD	password727
31	Walter	Gonzalez	7442-5566	walter.gonzalez3@catolica.edu.sv	mi casa	walter64#
\.


--
-- TOC entry 4761 (class 2606 OID 42395)
-- Name: categoria categoria_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categoria
    ADD CONSTRAINT categoria_pkey PRIMARY KEY (categoria_id);


--
-- TOC entry 4787 (class 2606 OID 116543)
-- Name: detalle_pedido_nuevo detalle_pedido_nuevo_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detalle_pedido_nuevo
    ADD CONSTRAINT detalle_pedido_nuevo_pkey PRIMARY KEY (pedido_nuevo_id, producto_nuevo_id);


--
-- TOC entry 4779 (class 2606 OID 116461)
-- Name: detalle_pedido detalle_pedido_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detalle_pedido
    ADD CONSTRAINT detalle_pedido_pkey PRIMARY KEY (pedido_id, producto_id);


--
-- TOC entry 4765 (class 2606 OID 42408)
-- Name: estado_lote estado_lote_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.estado_lote
    ADD CONSTRAINT estado_lote_pkey PRIMARY KEY (estado_id);


--
-- TOC entry 4769 (class 2606 OID 42435)
-- Name: estado_stock estado_stock_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.estado_stock
    ADD CONSTRAINT estado_stock_pkey PRIMARY KEY (estado_id);


--
-- TOC entry 4789 (class 2606 OID 141270)
-- Name: extraccion_producto extraccion_producto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.extraccion_producto
    ADD CONSTRAINT extraccion_producto_pkey PRIMARY KEY (extraccion_id);


--
-- TOC entry 4773 (class 2606 OID 42496)
-- Name: inventario inventario_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventario
    ADD CONSTRAINT inventario_pkey PRIMARY KEY (inventario_id);


--
-- TOC entry 4767 (class 2606 OID 42449)
-- Name: lote lote_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lote
    ADD CONSTRAINT lote_pkey PRIMARY KEY (lote_id);


--
-- TOC entry 4781 (class 2606 OID 116493)
-- Name: pedido_nuevo pedido_nuevo_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pedido_nuevo
    ADD CONSTRAINT pedido_nuevo_pkey PRIMARY KEY (pedido_nuevo_id);


--
-- TOC entry 4777 (class 2606 OID 116446)
-- Name: pedido pedido_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pedido
    ADD CONSTRAINT pedido_pkey PRIMARY KEY (pedido_id);


--
-- TOC entry 4785 (class 2606 OID 116528)
-- Name: producto_nuevo producto_nuevo_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.producto_nuevo
    ADD CONSTRAINT producto_nuevo_pkey PRIMARY KEY (producto_nuevo_id);


--
-- TOC entry 4763 (class 2606 OID 42415)
-- Name: producto producto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.producto
    ADD CONSTRAINT producto_pkey PRIMARY KEY (producto_id);


--
-- TOC entry 4759 (class 2606 OID 42390)
-- Name: proveedor proveedor_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.proveedor
    ADD CONSTRAINT proveedor_pkey PRIMARY KEY (proveedor_id);


--
-- TOC entry 4783 (class 2606 OID 116523)
-- Name: registro registro_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.registro
    ADD CONSTRAINT registro_pkey PRIMARY KEY (registro_id);


--
-- TOC entry 4771 (class 2606 OID 42456)
-- Name: stock stock_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stock
    ADD CONSTRAINT stock_pkey PRIMARY KEY (stock_id);


--
-- TOC entry 4775 (class 2606 OID 116441)
-- Name: tipo_pago tipo_pago_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tipo_pago
    ADD CONSTRAINT tipo_pago_pkey PRIMARY KEY (tipo_pago_id);


--
-- TOC entry 4757 (class 2606 OID 42383)
-- Name: usuario usuario_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (usuario_id);


--
-- TOC entry 4804 (class 2606 OID 116544)
-- Name: detalle_pedido_nuevo detalle_pedido_nuevo_pedido_nuevo_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detalle_pedido_nuevo
    ADD CONSTRAINT detalle_pedido_nuevo_pedido_nuevo_id_fkey FOREIGN KEY (pedido_nuevo_id) REFERENCES public.pedido_nuevo(pedido_nuevo_id);


--
-- TOC entry 4805 (class 2606 OID 116549)
-- Name: detalle_pedido_nuevo detalle_pedido_nuevo_producto_nuevo_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detalle_pedido_nuevo
    ADD CONSTRAINT detalle_pedido_nuevo_producto_nuevo_id_fkey FOREIGN KEY (producto_nuevo_id) REFERENCES public.producto_nuevo(producto_nuevo_id);


--
-- TOC entry 4798 (class 2606 OID 116462)
-- Name: detalle_pedido detalle_pedido_pedido_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detalle_pedido
    ADD CONSTRAINT detalle_pedido_pedido_id_fkey FOREIGN KEY (pedido_id) REFERENCES public.pedido(pedido_id);


--
-- TOC entry 4799 (class 2606 OID 116467)
-- Name: detalle_pedido detalle_pedido_producto_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detalle_pedido
    ADD CONSTRAINT detalle_pedido_producto_id_fkey FOREIGN KEY (producto_id) REFERENCES public.producto(producto_id);


--
-- TOC entry 4806 (class 2606 OID 141276)
-- Name: extraccion_producto extraccion_producto_lote_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.extraccion_producto
    ADD CONSTRAINT extraccion_producto_lote_id_fkey FOREIGN KEY (lote_id) REFERENCES public.lote(lote_id);


--
-- TOC entry 4807 (class 2606 OID 141271)
-- Name: extraccion_producto extraccion_producto_producto_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.extraccion_producto
    ADD CONSTRAINT extraccion_producto_producto_id_fkey FOREIGN KEY (producto_id) REFERENCES public.producto(producto_id);


--
-- TOC entry 4792 (class 2606 OID 42426)
-- Name: lote lote_lote_estado_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lote
    ADD CONSTRAINT lote_lote_estado_fkey FOREIGN KEY (lote_estado) REFERENCES public.estado_lote(estado_id);


--
-- TOC entry 4793 (class 2606 OID 42421)
-- Name: lote lote_producto_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lote
    ADD CONSTRAINT lote_producto_id_fkey FOREIGN KEY (producto_id) REFERENCES public.producto(producto_id);


--
-- TOC entry 4800 (class 2606 OID 116494)
-- Name: pedido_nuevo pedido_nuevo_proveedor_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pedido_nuevo
    ADD CONSTRAINT pedido_nuevo_proveedor_id_fkey FOREIGN KEY (proveedor_id) REFERENCES public.proveedor(proveedor_id);


--
-- TOC entry 4801 (class 2606 OID 116499)
-- Name: pedido_nuevo pedido_nuevo_tipo_pago_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pedido_nuevo
    ADD CONSTRAINT pedido_nuevo_tipo_pago_id_fkey FOREIGN KEY (tipo_pago_id) REFERENCES public.tipo_pago(tipo_pago_id);


--
-- TOC entry 4796 (class 2606 OID 116447)
-- Name: pedido pedido_proveedor_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pedido
    ADD CONSTRAINT pedido_proveedor_id_fkey FOREIGN KEY (proveedor_id) REFERENCES public.proveedor(proveedor_id);


--
-- TOC entry 4797 (class 2606 OID 116452)
-- Name: pedido pedido_tipo_pago_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pedido
    ADD CONSTRAINT pedido_tipo_pago_id_fkey FOREIGN KEY (tipo_pago_id) REFERENCES public.tipo_pago(tipo_pago_id);


--
-- TOC entry 4790 (class 2606 OID 42399)
-- Name: producto producto_categoria_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.producto
    ADD CONSTRAINT producto_categoria_id_fkey FOREIGN KEY (categoria_id) REFERENCES public.categoria(categoria_id);


--
-- TOC entry 4791 (class 2606 OID 42499)
-- Name: producto producto_inventario_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.producto
    ADD CONSTRAINT producto_inventario_id_fkey FOREIGN KEY (inventario_id) REFERENCES public.inventario(inventario_id);


--
-- TOC entry 4802 (class 2606 OID 116529)
-- Name: producto_nuevo producto_nuevo_categoria_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.producto_nuevo
    ADD CONSTRAINT producto_nuevo_categoria_id_fkey FOREIGN KEY (categoria_id) REFERENCES public.categoria(categoria_id);


--
-- TOC entry 4803 (class 2606 OID 116534)
-- Name: producto_nuevo producto_nuevo_estado_registro_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.producto_nuevo
    ADD CONSTRAINT producto_nuevo_estado_registro_fkey FOREIGN KEY (estado_registro) REFERENCES public.registro(registro_id);


--
-- TOC entry 4794 (class 2606 OID 42462)
-- Name: stock stock_lote_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stock
    ADD CONSTRAINT stock_lote_id_fkey FOREIGN KEY (lote_id) REFERENCES public.lote(lote_id);


--
-- TOC entry 4795 (class 2606 OID 42467)
-- Name: stock stock_stock_estado_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stock
    ADD CONSTRAINT stock_stock_estado_fkey FOREIGN KEY (stock_estado) REFERENCES public.estado_stock(estado_id);


-- Completed on 2024-11-16 14:30:56

--
-- PostgreSQL database dump complete
--

